


/*event listener for when habit added
document.querySelector('#HabitSubmit').addEventListener('click',function()  
{
    habitName = ($('#habit')[0].value);

    if(habitName != ""){
        document.querySelector('#habitQ').innerHTML = "How would you like to track '"+habitName+"'?"
        $('#HabitDetails')[0].hidden=false;
    }
});*/




/* function called when store habit button pressed
function StoreHabit(habit)
{
    let AllKeys=Object.keys(localStorage);
    if(AllKeys.length!=0){
        for (var i=0; AllKeys.length-1;i++){
            let key=AllKeys[i];
            if(localStorage.getItem(key)==newHabit.Name)
            {
                console.log('Habit already created');
                return false;
            }
        }
    }
    localStorage.setItem(habitName,JSON.stringify(newHabit));
} */

/*event listener for final habit submit button*
$('#HabitSubmit')[0].addEventListener('click',function()
{
    newHabit.Description = $('#DescriptionInput')[0].value;
    StoreHabit(newHabit);
});

/*create text file */
