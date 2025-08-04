document.addEventListener("DOMContentLoaded", function () {
  const items = document.querySelectorAll(".timeline-item");

  const revealTimeline = () => {
    const triggerBottom = window.innerHeight * 0.85;

    items.forEach((item) => {
      const itemTop = item.getBoundingClientRect().top;
      if (itemTop < triggerBottom) {
        item.classList.add("reveal");
      }
    });
  };

  window.addEventListener("scroll", revealTimeline);
  revealTimeline(); // Initial call in case items are already in view
});
