
const helper = {
    formatMoney(amount) {
        return amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")+ " ₫";
    },
    checkImagePath(valueId) {
        if (valueId) {
            return "https://localhost:7242/" + valueId;

        } else {
            return "https://localhost:7242/images/no-image.jpg";
        }
    },
}
export default helper ;