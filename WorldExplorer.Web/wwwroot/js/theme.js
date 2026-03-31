(function () {
    var defaultTheme = 'dark';
    var theme = localStorage.getItem('theme') || defaultTheme;
    document.documentElement.setAttribute('data-theme', theme);
})();