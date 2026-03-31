(function () {
    var defaultTheme = 'light';
    var theme = localStorage.getItem('theme') || defaultTheme;
    document.documentElement.setAttribute('data-theme', theme);
})();