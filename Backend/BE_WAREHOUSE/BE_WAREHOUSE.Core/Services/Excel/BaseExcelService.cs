using AutoMapper;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Excel;
using BE_WAREHOUSE.Core.Model.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.Excel
{
    public abstract class BaseExcelService<TEntity, TDto> : ExcelService<TDto, TEntity>, IBaseExcelService<TEntity>
    {
        IBaseRepository<TEntity> _baseRepository;
        protected IMapper _mapper;

        public BaseExcelService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<byte[]> ExportAllAsync(ExcelRequest<TEntity> excelRequest)
        {
            var data = await _baseRepository.GetAllAsync();
            var dataEnities = data.Select(item => MapEntityToDto(item));
            excelRequest.Columns = GetColumns();
            var bytes = ExportExcelAsync(dataEnities, excelRequest);
            return bytes;
        }

        public async Task<byte[]> ExportListAsync(ExcelRequest<TEntity> excelRequest)
        {
            var data = await _baseRepository.GetByIdsAsync(excelRequest.EntityIds);
            excelRequest.Columns = GetColumns();
            var dataEnities = data.Select(item => MapEntityToDto(item));
            var bytes = ExportExcelAsync(dataEnities, excelRequest);
            return bytes;
        }

        public TDto MapEntityToDto(TEntity entity)
        {
            var entityDto = _mapper.Map<TDto>(entity);
            return entityDto;
        }
        public abstract List<String> GetColumns();

    }
}
