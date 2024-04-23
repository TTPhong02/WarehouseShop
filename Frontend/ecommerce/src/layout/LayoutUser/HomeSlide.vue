<template>
  <div class="slideshow-container">
    <div class="slideshow">
      <div v-for="(slide, index) in slides" :key="index" class="slide" :style="{ opacity: index === currentSlideIndex ? 1 : 0 }">
        <img :src="slide.image" alt="Slide Image" class="slide-image" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name:"HomeSlide",
  data() {
    return {
      slides: [
        { image: require('@/assets/img/home-slide/slider_1.jpg') },
        { image: require('@/assets/img/home-slide/slider_2.jpg') },
        { image: require('@/assets/img/home-slide/slider_3.jpg') },
        // Add more slides as needed
      ],
      currentSlideIndex: 0,
      slideInterval: null
    };
  },
  mounted() {
    this.startSlideShow();
  },
  methods: {
    startSlideShow() {
      this.slideInterval = setInterval(() => {
        this.nextSlide();
      }, 4000); // Thời gian chuyển đổi giữa các slide (3 giây)
    },
    nextSlide() {
      this.currentSlideIndex = (this.currentSlideIndex + 1) % this.slides.length;
    },
    prevSlide() {
      if (this.currentSlideIndex === 0) {
        this.currentSlideIndex = this.slides.length - 1;
      } else {
        this.currentSlideIndex--;
      }
    }
  },
  beforeUnmount() {
    clearInterval(this.slideInterval);
  }
};
</script>

<style>
.slideshow-container {
  position: relative;
  width: 100%;
  max-width: 100%;
  overflow: hidden;
}

.slideshow {
  position: relative;
  width: 100%;
  height: 400px; /* Điều chỉnh chiều cao của slideshow */
}

.slide {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  opacity: 0;
  transition: opacity 1s ease;
}

.slide-image {
  width: 100%;
  height: 100%;
  object-fit: cover; /* Đảm bảo ảnh vừa khít với kích thước của slide */
}
</style>
