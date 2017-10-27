/* Load the HTTP library */
var http = require("http");

/* Create an HTTP server to handle responses */
http.createServer(function(request, response) {
  response.writeHead(200, {"Content-Type": "text/plain"});
  response.write("Hello World");
  response.end();
}).listen(8888);


var counter=true;

$("#button").click(function(){
if(counter){
    document.getElementById('spotify').height="400";
    counter=false;
    }
else{
    document.getElementById('spotify').height="200";
    counter=true;
    }
});
