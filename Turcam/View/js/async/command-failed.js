var area = $("textarea#signal-area");
var value = area.text();
area.text("Failed: {0}\r\n" + value);