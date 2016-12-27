$(document).ready(function () {
    var layoutGrid = function () {
        $("#grid").height($(window).height() * .7);

        var gridElement = $("#grid"),
            dataArea = gridElement.find(".k-grid-content"),
            gridHeight = gridElement.innerHeight(),
            otherElements = gridElement.children().not(".k-grid-content").not(".k-grid-header").not(".k-grid-footer"),
            otherElementsHeight = 0;
        otherElements.each(function () {
            otherElementsHeight += $(this).outerHeight();
        });
        dataArea.css("min-height", function () {
            return $("#grid").height() * .4;
        });
        dataArea.height(gridHeight - otherElementsHeight);
    };

    window.setTimeout(function () { layoutGrid(); }, 1);

    $(window).resize(function () {
        window.setTimeout(function () { layoutGrid(); }, 1);
    });
});