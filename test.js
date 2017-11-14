
$(document).ready(function(){
    $('#testButton').click(function(){
        var val = $('#testButton').val();

        $.ajax({
            type:'Post',
            url:"/Habit/TestPost",
            data:{ 
                Name:'test',
                Time:'01:00:00',
                Description:'please work',
                DoneOrNot:'0'
            },
            success: function(data){
                alert(data.result);
            }


        });

    


    });

});
  