
XMLButton=document.getElementById("btn_2");
XMLButton.addEventListener('click',FetchObjects);

function FetchObjects(){
xml=new XMLHttpRequest();
xml.onreadystatechange= function(){
    user_data_container=document.createElement("div");
    if(this.readyState==4 && this.status==200){
        console.log("loading");
        items=JSON.parse(this.responseText);
        for(i=0; i<10; i++){
            
            console.log(items[i].title);
            user_data_container.innerHTML=`<p></p>`;
        }
    }
}
xml.open('GET',"https://jsonplaceholder.typicode.com/posts",true); // async programming true
xml.send();
}
