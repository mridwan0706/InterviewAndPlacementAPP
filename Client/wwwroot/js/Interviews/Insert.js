
function CekName() {  
    debugger;
    var IdParticipant = $("#InvitationInterview").val();    
    var obj = new Object();   
    obj.ParticipantId = IdParticipant;
        $.ajax({            
            "url": "/Interviews/GetNameParticipant/",
                "type": "GET",
                "dataType": "json",
                "data": { ParticipantId: obj.ParticipantId[0]}
        }).then((result)=>{
            debugger;
            if(result.data[0] != 0){
                var data = result.data;
                var InterviewVM = JSON.parse(data);
                var ParticipantName = InterviewVM[0].participant;
                var Date = $(".InterviewDate").val();
                var Time = $("#InterviewTime").val();                   
                var interview = new Object();
                interview.Status = "Waiting Confirmation";    
                interview.InterviewDate = Date + " " + Time;    
                interview.Note = $("#NoteText").val(); 
                interview.SiteId = $("#SiteText").val(); 
                interview.ParticipantId = IdParticipant;
                interview.Participant = ParticipantName;

                $.ajax({
                    "url": "/Interviews/Insert",
                    "type": "Post",
                    "dataType": "json",
                    "data": interview
                }).then((result) => {
                    debugger
                    if (result.statusCode == 200) {
                        Swal.fire(
                            'Success!',
                            'Data Inserted Success',
                            'success').then(() => {
                                $("#example23").DataTable().ajax.reload();
                            })
                    }
                    else {
                        Swal.fire(
                            'Failed!',
                            'Oops.',
                            'failed'
                        )
                    }
                })          
            }
        })
}



    
    
    