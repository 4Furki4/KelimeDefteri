
$("document").ready(function () {
    // Animate inputs when page loaded
    $("#part-1").hide().slideDown(1000);
    $("#part-2").hide().slideDown(1000);
    $("#part-3").hide().slideDown(1000);
    $("#part-4").hide().slideDown(1000);
    $("#dateInput").hide().slideDown(1000);
    $("#ekleBtn").hide().slideDown();
    // int vars indicates how many times new inputs added. (I couldn't figure out how to decrease them when the one is deleted.)
    let firstBtnCount = 1;
    let secondBtnCount = 1;
    let thirdBtnCount = 1;
    let fourthBtnCount = 1;
    $("#part-1-btn").on("click", function () { // Calling addInputs method when the buttons clicked and increase the btn count vars
        addInputs(firstBtnCount, "part-1", 1);
        firstBtnCount++;
    });
    $("#part-2-btn").on("click", function () {
        addInputs(secondBtnCount, "part-2", 2);
        secondBtnCount++;
    });
    $("#part-3-btn").on("click", function () {
        addInputs(thirdBtnCount, "part-3", 3);
        thirdBtnCount++;
    });
    $("#part-4-btn").on("click", function () {
        addInputs(fourthBtnCount, "part-4", 4);
        fourthBtnCount++;
    });
    function addInputs(counter, part, wordOrder) {
        let delBtnOrder = `${part}-del-btn-${counter}`;
        let defId = `word${wordOrder}-def${counter}`;
        let typeId = `word${wordOrder}-type${counter}`;
        let appendPart = `#${part}`;
        // the rows to be added when add new definition('Tanım Ekle') button clicked
        let row = `
        <div id="${part}-row-${counter}" class="row">
            <div class="offset-3 col-4" id="${part}-def">
                <input type="text" class="form-control mt-1" id="word${wordOrder}-def${counter}" placeholder="Tanım giriniz..." name="newDefinition${wordOrder}">
            </div>
            <div class="col-3" id="${part}-type">
                <input type="text" class="form-control mt-1" id="word${wordOrder}-type${counter}" placeholder="Tür giriniz..." name="newType${wordOrder}">
            </div>
            <button id="${delBtnOrder}" class="btn mt-1" type="button" style="width: 60px; height: 40px;"><img src="/images/icons8-trash-bin-50.png" width="25px" height="25px" alt=""></button>
        </div>`

        $(appendPart).append(row);
                                    //Adding some animations...
        $("#" + defId).hide().show("slow");
        $("#" + typeId).hide().show("slow"); 
        $(`#${delBtnOrder}`).hide().slideDown("slow")
                                    //Delete button
        $(`#${delBtnOrder}`).on("click", function () {
            $(`#${part}-row-${counter}`).slideUp("slow").empty(); //Delete input with animation
        });
        
    }
});