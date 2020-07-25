/*
 * Sweet alerts 2
 */

!function ($) {
    "use strict";

    var SweetAlert = function () {
    };

    //examples
    SweetAlert.prototype.init = function () {

        //Basic
        $('#sa-basic').on('click', function () {
            swal(
                {
                    title: 'Basic example of Sweet alerts!',
                    confirmButtonClass: 'btn btn-confirm mt-2'
                }
            ).catch(swal.noop)
        });

        //A title with a text under
        $('#sa-title').click(function () {
            swal(
                {
                    title: "The Internet?",
                    text: 'That thing is still around?',
                    type: 'question',
                    confirmButtonClass: 'btn btn-confirm mt-2'
                }
            )
        });

        //Success Message
        $('#sa-success').click(function () {
            swal(
                {
                    title: 'Good job!',
                    text: 'You clicked the button!',
                    type: 'success',
                    confirmButtonClass: 'btn btn-confirm mt-2'
                }
            )
        });

        //Warning Message
        $('#sa-warning').click(function () {
            swal({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonClass: 'btn btn-confirm mt-2',
                cancelButtonClass: 'btn btn-secondary ml-2 mt-2',
                confirmButtonText: 'Yes, delete it!'
            }).then(function () {
                swal({
                        title: 'Deleted !',
                        text: "Your file has been deleted",
                        type: 'success',
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    }
                )
            })
        });

        //Parameter
        $('#sa-params').click(function () {
            swal({
                title: 'Estas Seguro?',
                text: "No Es Posible Revertir",
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Si, Borrar',
                cancelButtonText: 'No, Cancel!',
                confirmButtonClass: 'btn btn-success mt-2',
                cancelButtonClass: 'btn btn-danger ml-2 mt-2',
                buttonsStyling: false
            }).then(function () {
                $.ajax({
                    async: false,
                    type: 'POST',
                    url: '/ContactMessage/Eliminar/',
                    data: JSON.stringify({ id: document.getElementById('MessageId').value } ),
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        if (data === 'true') {
                            swal({
                                title: 'Eliminado!',
                                text: "El Elemento ha sido Eliminado",
                                type: 'success',
                                confirmButtonClass: 'btn btn-confirm mt-2'
                            }).then(function () {
                                window.location = "/ContactMessage/Index";
                            });
                        }
                    },
                    error: function (xhr, type, exception) {
                        console.log(data)
                        swal({
                            title: 'Error',
                            text: "ajax error response type " + type + ' \nException: ' + exception + ' \nXhr: ' + xhr,
                            type: 'error',
                            confirmButtonClass: 'btn btn-confirm mt-2'
                        })
                        // if ajax fails display error alert
                        //console.log('Mierda no jalo X.X ' + exception + xhr);
                    }
                });
                //.then(function () {
                   
                //    //------------------------------------------ Same Logic as Ajax Call without POST Type
                //    //var url = '/ContactMessage/Eliminar/' + $('#MessageId').val();
                //    //window.location.href = url;
                //    //------------------------------------------ Same Logic as Ajax Call without POST Type
                //    //var url = $("#RedirectTo").val();
                //    //location.href = url;
                //});  
            }, function (dismiss) {
                // dismiss can be 'cancel', 'overlay',
                // 'close', and 'timer'
                if (dismiss === 'cancel') {
                    swal({
                        title: 'Cancelado',
                        text: "El Elemento Queda Intacto",
                        type: 'error',
                        confirmButtonClass: 'btn btn-confirm mt-2'
                    })
                }
            })
        });

        //Custom Image
        $('#sa-image').click(function () {
            swal({
                title: 'Sweet!',
                text: 'Modal with a custom image.',
                imageUrl: 'images/avatar2.jpg',
                imageHeight: 70,
                animation: false,
                confirmButtonClass: 'btn btn-confirm mt-2'
            })
        });

        //Auto Close Timer
        $('#sa-close').click(function () {
            swal({
                title: 'Auto close alert!',
                text: 'I will close in 2 seconds.',
                timer: 2000,
                confirmButtonClass: 'btn btn-confirm mt-2'
            }).then(
                function () {
                },
                // handling the promise rejection
                function (dismiss) {
                    if (dismiss === 'timer') {
                        console.log('I was closed by the timer')
                    }
                }
            )
        });

        //custom html alert
        $('#custom-html-alert').click(function () {
            swal({
                title: '<i>HTML</i> <u>example</u>',
                type: 'info',
                html: 'You can use <b>bold text</b>, ' +
                '<a href="//bootstraplovers.com/">links</a> ' +
                'and other HTML tags',
                showCloseButton: true,
                showCancelButton: true,
                confirmButtonClass: 'btn btn-confirm mt-2',
                cancelButtonClass: 'btn btn-secondary ml-2 mt-2',
                confirmButtonText: '<i class="fa fa-thumbs-up"></i> Great!',
                cancelButtonText: '<i class="fa fa-thumbs-down"></i>'
            })
        });

        //Custom width padding
        $('#custom-padding-width-alert').click(function () {
            swal({
                title: 'Custom width, padding, background.',
                width: 600,
                padding: 100,
                confirmButtonClass: 'btn btn-confirm mt-2',
                background: '#fff url(//subtlepatterns2015.subtlepatterns.netdna-cdn.com/patterns/geometry.png)'
            })
        });

        //Ajax
        $('#ajax-alert').click(function () {
            swal({
                title: 'Submit email to run ajax request',
                input: 'email',
                showCancelButton: true,
                confirmButtonText: 'Submit',
                showLoaderOnConfirm: true,
                confirmButtonClass: 'btn btn-confirm mt-2',
                cancelButtonClass: 'btn btn-secondary ml-2 mt-2',
                preConfirm: function (email) {
                    return new Promise(function (resolve, reject) {
                        setTimeout(function () {
                            if (email === 'taken@example.com') {
                                reject('This email is already taken.')
                            } else {
                                resolve()
                            }
                        }, 2000)
                    })
                },
                allowOutsideClick: false
            }).then(function (email) {
                swal({
                    type: 'success',
                    title: 'Ajax request finished!',
                    html: 'Submitted email: ' + email,
                    confirmButtonClass: 'btn btn-confirm mt-2'
                })
            })
        });

        //chaining modal alert
        $('#chaining-alert').click(function () {
            swal.setDefaults({
                input: 'text',
                confirmButtonText: 'Next &rarr;',
                showCancelButton: true,
                animation: false,
                progressSteps: ['1', '2', '3'],
                confirmButtonClass: 'btn btn-confirm mt-2',
                cancelButtonClass: 'btn btn-secondary ml-2 mt-2'
            })

            var steps = [
                {
                    title: 'Question 1',
                    text: 'Chaining swal2 modals is easy'
                },
                'Question 2',
                'Question 3'
            ]

            swal.queue(steps).then(function (result) {
                swal.resetDefaults()
                swal({
                    title: 'All done!',
                    confirmButtonClass: 'btn btn-confirm mt-2',
                    html: 'Your answers: <pre>' +
                    JSON.stringify(result) +
                    '</pre>',
                    confirmButtonText: 'Lovely!',
                    showCancelButton: false
                })
            }, function () {
                swal.resetDefaults()
            })
        });

        //Danger
        $('#dynamic-alert').click(function () {
            swal.queue([{
                title: 'Your public IP',
                confirmButtonText: 'Show my public IP',
                confirmButtonClass: 'btn btn-confirm mt-2',
                text: 'Your public IP will be received ' +
                'via AJAX request',
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        $.get('https://api.ipify.org?format=json')
                            .done(function (data) {
                                swal.insertQueueStep(data.ip)
                                resolve()
                            })
                    })
                }
            }])
        });


    },
        //init
        $.SweetAlert = new SweetAlert, $.SweetAlert.Constructor = SweetAlert
}(window.jQuery),

//initializing
    function ($) {
        "use strict";
        $.SweetAlert.init()
    }(window.jQuery);