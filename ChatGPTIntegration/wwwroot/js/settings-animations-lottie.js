// Carrega a animação da gravação
var animationRecoding = bodymovin.loadAnimation({
    container: document.getElementById('animation-recording'),
    renderer: 'svg',
    loop: true,
    autoplay: true,
    path: 'animation/recording-animation-button.json'
});