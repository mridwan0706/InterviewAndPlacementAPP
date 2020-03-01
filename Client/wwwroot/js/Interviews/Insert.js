function Insert() {
    debugger;
    var interview = new Object();
    interview.Status = "Waiting";
    interview.InterviewDate = $(".InterviewDate").val();
    interview.InterviewTime = $("#InterviewTime").val();
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
                'Data Success Inserted Success',
                'success').then(() => {
                    $("#example23").DataTable().ajax.reload();
                });
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