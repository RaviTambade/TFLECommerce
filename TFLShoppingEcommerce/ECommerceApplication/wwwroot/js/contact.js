document.addEventListener("DOMContentLoaded", () => {
  const elements = document.querySelectorAll(
    ".animate-fade, .animate-slide-up"
  );

  const revealOnScroll = () => {
    const triggerBottom = window.innerHeight * 0.85;
    elements.forEach((el) => {
      const boxTop = el.getBoundingClientRect().top;
      if (boxTop < triggerBottom) {
        el.classList.add("reveal");
      }
    });
  };

  window.addEventListener("scroll", revealOnScroll);
  revealOnScroll(); // initial check
});
