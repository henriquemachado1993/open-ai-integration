if ("webkitSpeechRecognition" in window) {

    let speechRecognition = new webkitSpeechRecognition();

    // configurações
    speechRecognition.lang = 'pt-BR';
    speechRecognition.interimResults = true;
    speechRecognition.continuous = true;

    speechRecognition.onstart = () => {
        document.querySelector("#animation-recording").style.display = "block";
        document.querySelector("#btn-icon-recording").style.display = "none";
    };
    speechRecognition.onerror = () => {
        document.querySelector("#animation-recording").style.display = "none";
        document.querySelector("#btn-icon-recording").style.display = "block";
    };
    speechRecognition.onend = () => {
        document.querySelector("#animation-recording").style.display = "none";
        document.querySelector("#btn-icon-recording").style.display = "block";
    };

    let transcript = "";

    speechRecognition.onresult = (event) => {
        let interimTranscript = "";
        for (let i = event.resultIndex; i < event.results.length; ++i) {
            if (event.results[i].isFinal)
                transcript += event.results[i][0].transcript;
            else
                interimTranscript += event.results[i][0].transcript;
        }

        document.querySelector("#message-user").value = transcript;
    };

    document.querySelector("#record").onclick = () => {
        if (document.querySelector("#aux-recording").value == "stopped") {

            document.querySelector("#aux-recording").value = "recording";
            speechRecognition.start();

            document.querySelector("#message-user").value = "";
        } else {
            document.querySelector("#aux-recording").value = "stopped";
            speechRecognition.stop();
            transcript = "";
        }
    };
} else {
    alert("Nenhum reconhecimento de fala não disponível");
}