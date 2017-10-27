/*handling button clicks on habit type*/
function HabitTypeClick(name){
            if(name=="Frequency"){
                console.log('frequencyyy');
                if(typeof(window.localStorage) != "undefined")
            {
                console.log('Theres Storage!');
            }
            else
            {
                console.log('No storage');
            }
        }
        };

var newHabit={};
var habitName;

/*event listener for when habit added*/
document.querySelector('#HabitSubmit').addEventListener('click',function()  
{
    habitName = ($('#habit')[0].value);

    if(habitName != ""){
        document.querySelector('#habitQ').innerHTML = "How would you like to track '"+habitName+"'?"
        $('#HabitDetails')[0].hidden=false;
    }
});

newHabit.Times=[];
/*function called when add time button pushed*/
function PostTimes(time) 
{   if(time!=""){
        $('#TimesPosting')[0].insertAdjacentHTML('beforeend',time+',');
        newHabit.Times.push(time);
        }
};

/*event listener for add times button*/
document.querySelector('#AddTime').addEventListener('click',function(){PostTimes($('#TimeSelect')[0].value)});


/* function called when store habit button pressed*/
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
}

/*event listener for final habit submit button*/
document.querySelector('#HabitFinalSubmit').addEventListener('click',function()
{
    newHabit.Description = $('#DescriptionInput')[0].value;
    StoreHabit(newHabit);
});


/*create text file */
