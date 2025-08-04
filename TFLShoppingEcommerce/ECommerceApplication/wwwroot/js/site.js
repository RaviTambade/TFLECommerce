// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function toggleSidebar() {
  const sidebar = document.querySelector(".sidebar");
  sidebar.classList.toggle("open");
}
  

document.addEventListener("DOMContentLoaded", function () {
  const cards = document.querySelectorAll(".card");

  const observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add("visible");
        }
      });
    },
    {
      threshold: 0.1,
    }
  );

  cards.forEach((card) => observer.observe(card));
});


document.addEventListener("DOMContentLoaded", function () {
  const animatedCards = document.querySelectorAll(".card, .review-card");

  const observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add("visible");
        }
      });
    },
    {
      threshold: 0.1,
    }
  );

  animatedCards.forEach((card) => observer.observe(card));
});



document.addEventListener("DOMContentLoaded", () => {
  const sections = document.querySelectorAll(".reveal-section");

  const reveal = () => {
    sections.forEach((section) => {
      const position = section.getBoundingClientRect().top;
      if (position < window.innerHeight - 100) {
        section.classList.add("reveal");
      }
    });
  };

  reveal();
  window.addEventListener("scroll", reveal);

  // OPTIONAL: Dynamically set timeline height
  const timeline = document.querySelector(".timeline");
  const items = document.querySelectorAll(".timeline-item");
  const itemSpacing = 150;
  if (timeline) {
    timeline.style.minHeight = `${items.length * itemSpacing}px`;
  }
});
