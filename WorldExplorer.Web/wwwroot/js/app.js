window.registerClickOutside = (element, dotNetRef) => {
    document.addEventListener('click', (e) => {
        if (!element.contains(e.target)) {
            dotNetRef.invokeMethodAsync('CloseDropdown');
        }
    });
};

window.registerScrollListener = (dotNetRef) => {
    window.addEventListener('scroll', () => {
        const isVisible = window.scrollY > 300;
        dotNetRef.invokeMethodAsync('UpdateVisibility', isVisible);
    });
};

window.scrollToTop = () => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
};