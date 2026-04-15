(function () {
    "use strict";

    /**
     * Apply .scrolled class to the body as the page is scrolled down
     */
    function toggleScrolled() {
        const selectBody = document.querySelector('body');
        const selectHeader = document.querySelector('#header');

        if (!selectHeader) return;

        if (
            !selectHeader.classList.contains('scroll-up-sticky') &&
            !selectHeader.classList.contains('sticky-top') &&
            !selectHeader.classList.contains('fixed-top')
        ) return;

        window.scrollY > 100
            ? selectBody.classList.add('scrolled')
            : selectBody.classList.remove('scrolled');
    }

    document.addEventListener('scroll', toggleScrolled);
    window.addEventListener('load', toggleScrolled);

    /**
     * Mobile nav toggle
     */
    const mobileNavToggleBtn = document.querySelector('.mobile-nav-toggle');

    function mobileNavToogle() {
        document.querySelector('body').classList.toggle('mobile-nav-active');

        if (mobileNavToggleBtn) {
            mobileNavToggleBtn.classList.toggle('bi-list');
            mobileNavToggleBtn.classList.toggle('bi-x');
        }
    }

    if (mobileNavToggleBtn) {
        mobileNavToggleBtn.addEventListener('click', mobileNavToogle);
    }

    /**
     * Hide mobile nav on same-page/hash links
     */
    document.querySelectorAll('#navmenu a').forEach(navmenu => {
        navmenu.addEventListener('click', () => {
            if (document.querySelector('.mobile-nav-active')) {
                mobileNavToogle();
            }
        });
    });

    /**
     * Toggle mobile nav dropdowns
     */
    document.querySelectorAll('.navmenu .toggle-dropdown').forEach(navmenu => {
        navmenu.addEventListener('click', function (e) {
            e.preventDefault();
            this.parentNode.classList.toggle('active');
            this.parentNode.nextElementSibling.classList.toggle('dropdown-active');
            e.stopImmediatePropagation();
        });
    });

    /**
     * Preloader
     */
    const preloader = document.querySelector('#preloader');

    if (preloader) {
        window.addEventListener('load', () => {
            preloader.remove();
        });
    }

    /**
     * Scroll top button (FIXED)
     */
    let scrollTop = document.querySelector('.scroll-top');

    function toggleScrollTop() {
        if (scrollTop) {
            window.scrollY > 100
                ? scrollTop.classList.add('active')
                : scrollTop.classList.remove('active');
        }
    }

    if (scrollTop) {
        scrollTop.addEventListener('click', (e) => {
            e.preventDefault();
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });
    }

    window.addEventListener('load', toggleScrollTop);
    document.addEventListener('scroll', toggleScrollTop);

    /**
     * Animation on scroll
     */
    function aosInit() {
        if (typeof AOS !== "undefined") {
            AOS.init({
                duration: 600,
                easing: 'ease-in-out',
                once: true,
                mirror: false
            });
        }
    }

    window.addEventListener('load', aosInit);

    /**
     * GLightbox
     */
    if (typeof GLightbox !== "undefined") {
        GLightbox({
            selector: '.glightbox'
        });
    }

    /**
     * PureCounter
     */
    if (typeof PureCounter !== "undefined") {
        new PureCounter();
    }

    /**
     * Swiper
     */
    function initSwiper() {
        if (typeof Swiper === "undefined") return;

        document.querySelectorAll(".init-swiper").forEach(function (swiperElement) {
            let config = JSON.parse(
                swiperElement.querySelector(".swiper-config").innerHTML.trim()
            );

            new Swiper(swiperElement, config);
        });
    }

    window.addEventListener("load", initSwiper);

    /**
     * Fix scroll on load with hash
     */
    window.addEventListener('load', function () {
        if (window.location.hash) {
            let section = document.querySelector(window.location.hash);
            if (section) {
                setTimeout(() => {
                    let scrollMarginTop = getComputedStyle(section).scrollMarginTop;
                    window.scrollTo({
                        top: section.offsetTop - parseInt(scrollMarginTop),
                        behavior: 'smooth'
                    });
                }, 100);
            }
        }
    });

    /**
     * Navmenu Scrollspy (EL IMPORTANTE 🔥)
     */
    let navmenulinks = document.querySelectorAll('.navmenu a');

    function navmenuScrollspy() {
        navmenulinks.forEach(navmenulink => {

            if (!navmenulink.hash) return;

            let section = document.querySelector(navmenulink.hash);
            if (!section) return;

            let position = window.scrollY + 200;

            if (
                position >= section.offsetTop &&
                position <= (section.offsetTop + section.offsetHeight)
            ) {
                document.querySelectorAll('.navmenu a.active')
                    .forEach(link => link.classList.remove('active'));

                navmenulink.classList.add('active');
            } else {
                navmenulink.classList.remove('active');
            }

        });
    }

    window.addEventListener('load', navmenuScrollspy);
    document.addEventListener('scroll', navmenuScrollspy);

})();

document.addEventListener("DOMContentLoaded", function () {
    AOS.init({
        duration: 800,
        easing: 'slide',
        once: true
    });
});

document.addEventListener("DOMContentLoaded", function () {

    new Swiper('.swiper', {
        loop: true,
        speed: 600,
        autoplay: {
            delay: 5000
        },
        slidesPerView: 'auto',
        pagination: {
            el: '.swiper-pagination',
            clickable: true
        }
    });

});

document.addEventListener("DOMContentLoaded", function () {

    // TESTIMONIALS
    new Swiper('#testimonials .swiper', {
        loop: true,
        speed: 600,
        autoplay: { delay: 3000 },
        slidesPerView: 1,
        pagination: {
            el: '#testimonials .swiper-pagination',
            clickable: true
        }
    });

    // EVENTS
    new Swiper('#events .swiper', {
        loop: true,
        speed: 600,
        autoplay: { delay: 4000 },

        breakpoints: {
            320: { slidesPerView: 1 },
            1200: { slidesPerView: 3 }
        },

        pagination: {
            el: '#events .swiper-pagination',
            clickable: true
        }
    });

  

});

