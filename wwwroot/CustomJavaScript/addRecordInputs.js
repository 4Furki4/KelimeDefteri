let wordInput = document.createElement("select");

wordInput.id = "wordInput";

for(let i = 4; i<10; i++) {

    let option = document.createElement("option")
    option.value = i;
    option.text = i;
    wordInput.appendChild(option);
}