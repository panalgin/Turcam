var axis = "{0}";
var value = "{1}";

switch (axis) {{
    case "X":
        $("div#x-position").text(value); break;

    case "Y":
        $("div#y-position").text(value); break;

    case "Z":
        $("div#z-position").text(value); break;

    case "A":
        $("div#a-position").text(value); break;

    case "B":
        $("div#b-position").text(value); break;
}} // string.Format need to escape curly braces :P