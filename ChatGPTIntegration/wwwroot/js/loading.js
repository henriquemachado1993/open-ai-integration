var animationRecoding = bodymovin.loadAnimation({
    container: document.getElementById('lottie-container-loading'),
    renderer: 'svg',
    loop: true,
    autoplay: true,
    path: 'animation/three-dots-loading.json'
});

function startLogading() {
    document.getElementById('lottie-container-loading').classList.remove('stop-loading');
    document.getElementById('lottie-container-loading').classList.add('start-loading');
}

function stopLogading() {
    document.getElementById('lottie-container-loading').classList.add('stop-loading');
    document.getElementById('lottie-container-loading').classList.remove('start-loading');
}