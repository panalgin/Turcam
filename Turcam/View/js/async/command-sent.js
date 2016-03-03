var area = $("textarea#signal-area");
var value = area.text();
area.text("Sent: {0}\r\n" + value);