﻿


/*event listener for add times button*/
$('#AddTimeButton')[0].addEventListener('click',function(event){
    event.preventDefault();
    PostTimes($('#InputTime')[0].value);

});
var timesArray=[];
//When add time button pushed
function PostTimes(time) 
{   
    if(time!=""){
        $('#ShowTimes')[0].insertAdjacentHTML('beforeend','<div style="color:green">'+time+'</div>');    //div holds the times to add to DB
        timesArray.push(time);
        }
};

//Open Modal on create habit button
$('#OpenModalButton').click(function(event){ 
        event.preventDefault();
        if($('#habit').val() != ""){
            $('#OpenModalButton')[0].setAttribute('data-toggle','modal');   //set data toggle
            $('#HabitChange')[0].innerHTML="Describe:   " + $('#habit').val();
        }
        else{
            alert('Habit must have name');
            $('#OpenModalButton')[0].setAttribute('data-toggle','');    //reset data toggle for button (prevents being able ot open modal after already having name and then deleting it)
        }
        
});


//Save Habit button from modal, goes to the submit function below
$('#ModalButtonHabitSubmit').click(function(){ //activate form submit
        if($('#HabitForm').valid() ==true )  //if form valid
        {
            $('#ModalButtonHabitSubmit')[0].setAttribute("data-dismiss","modal");  //dismiss modal
            var habitinsert = document.createElement('div');        //adding habit name to list added habits
            habitinsert.innerHTML='<span style="color: green"> + '+ $('#habit').val() +' </span>';
            $('#ShowHabits')[0].insertAdjacentElement('beforeend',habitinsert);
            $('#HabitForm').submit();           //submit form to database
        }
});
   

//on habit form submit (once abive is clicked)
$('#HabitForm').on("submit",function(event){    
        event.preventDefault();
        var token = $("[name='__RequestVerificationToken']").val();
        
        //converting times array to string
        var timeString="";
        var doneOrNot="";
        for(var i=0;i<timesArray.length;i++){
            timeString = timeString.concat(timesArray[i]+',');
            doneOrNot = doneOrNot.concat('0'); //0 = not done, 1 =done
        }

        $.ajax(
            {
                type:"POST",
                url:"/Habit/Create",
                data: {
                    __RequestVerificationToken: token,
                    Name: $('#habit').val(),
                    Time: timeString,
                    DoneOrNot: doneOrNot,
                    Description: $('#HabitDescription').val()
                }
            }
        );
        $('#habit').val("");    //clear habit name input textbox
        $('#InputTime').val("");
        $('#HabitDescription').val("");
        $('#ShowTimes')[0].innerHTML = "";
        timesArray=[];  //clear times
        doneOrNot="";   // clear done or not
});