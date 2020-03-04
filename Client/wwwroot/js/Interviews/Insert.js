function Insert() {
    debugger;
    var Date = $(".InterviewDate").val();
    var Time = $("#InterviewTime").val();
    var interview = new Object();
    interview.Status = "Waiting Confirmation";    
    interview.InterviewDate = Date + " " + Time;    
    interview.Note = $("#NoteText").val(); 
    interview.SiteId = $("#SiteText").val(); 
    interview.EmployeeId = $("#InvitationInterview").val();  
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