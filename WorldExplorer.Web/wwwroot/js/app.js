window.registerClickOutside = (element, dotNetRef) => {
    const handler = (e) => {
        if (!element.contains(e.target)) {
            dotNetRef.invokeMethodAsync('CloseDropdown')
                .catch(() => { });
        }
    };
    document.addEventListener('click', handler);

    return () => document.removeEventListener('click', handler);
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