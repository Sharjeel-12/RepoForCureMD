const {XMLHttpRequest}=require('xmlhttprequest');
const xml = new XMLHttpRequest();
var result=null;
url="https://jsonplaceholder.typicode.com/posts/2";
xml.open('GET',url,true);

xml.onreadystatechange=function(){
    if(this.readyState==4 && this.status==200){
        result=this.responseText;
        console.log(result);
    }
    else{
        console.log(`Nothing found! Ready state: ${this.readyState}, status: ${this.readyState}`)
    }
}

xml.send();

