(function () {
    function hideLoader() {
        const loader = document.getElementById('app-loading');
        const app = document.getElementById('app');

        if (loader) {
            loader.style.opacity = '0';
            setTimeout(() => loader.style.display = 'none', 300);
        }

        if (app) {
            app.classList.add('ready');
        }
    }

    document.addEventListener('blazor:initialized', hideLoader);

    setTimeout(hideLoader, 500);
})();