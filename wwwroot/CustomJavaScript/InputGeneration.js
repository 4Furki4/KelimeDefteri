
$("document").ready(function () {
    // Animate inputs when page loaded
    $("#first-part").hide().slideDown("slow");
    $("#second-part").hide().slideDown("slow");
    $("#third-part").hide().slideDown("slow");
    $("#fourth-part").hide().slideDown("slow");
    let firstBtnCount = 1;
    let secondBtnCount = 1;
    let thirdBtnCount = 1;
    let fourthBtnCount = 1;
    $("#first-part-btn").on("click", function () {
        addInputs(firstBtnCount, "first-part", 0);
        firstBtnCount++;
    });
    $("#second-part-btn").on("click", function () {
        addInputs(secondBtnCount, "second-part", 1);
        secondBtnCount++;
    });
    $("#third-part-btn").on("click", function () {
        addInputs(thirdBtnCount, "third-part", 2);
        thirdBtnCount++;
    });
    $("#fourth-part-btn").on("click", function () {
        addInputs(fourthBtnCount, "fourth-part", 3);
        fourthBtnCount++;
    });
    function addInputs(counter, part, wordOrder) {
        let delBtnOrder = `${part}-del-btn-${counter}`;
        let defId = `word${wordOrder}-def${counter}`;
        let typeId = `word${wordOrder}-type${counter}`;
        let appendPart = `#${part}`;
        let row = `
        <div id="${part}-row-${counter}" class="row">
            <div class="offset-3 col-4" id="first-part-def">
                <input type="text" class="form-control mt-1" id="word${wordOrder}-def${counter}" placeholder="Tanım giriniz...">
            </div>
            <div class="col-3" id="first-part-type">
                <input type="text" class="form-control mt-1" id="word${wordOrder}-type${counter}" placeholder="Tür giriniz...">
            </div>
            <button id="${delBtnOrder}" class="btn mt-1" type="button" style="width: 60px; height: 40px;"><img src="/images/icons8-trash-bin-50.png" width="25px" height="25px" alt=""></button>
        </div>`
        $(appendPart).append(row);
        $("#" + defId).hide().show("slow");
        $("#" + typeId).hide().show("slow");
        $(`#${delBtnOrder}`).on("click", function () {
            $(`#${part}-row-${counter}`).slideUp("slow").empty();
        });
        $(`#${delBtnOrder}`).hide().slideDown("slow")
    }
});