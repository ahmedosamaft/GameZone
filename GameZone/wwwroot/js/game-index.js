let btns = [...document.getElementsByClassName("js-delete")];

const swal = Swal.mixin({
    customClass: {
        confirmButton: "btn btn-danger mx-2",
        cancelButton: "btn  btn-success"
    },
    buttonsStyling: false
});
async function deleteHandler() {
    let id = this.dataset.id;
    let choice = await swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    })
    if (choice.isConfirmed) {
        try {
            let res = await fetch(`/Games/Delete/${id}`, { method: "DELETE" });
            if (res.ok) {
                let timerInterval;
                Swal.fire({
                    title: "Deleted!",
                    html: "The game is deleted successfully!",
                    icon: "sucess",
                    timer: 2000,
                    timerProgressBar: true,
                    didOpen: () => {
                        Swal.showLoading();
                        const timer = Swal.getPopup().querySelector("b");
                        timerInterval = setInterval(() => {
                            timer.textContent = `${Swal.getTimerLeft()}`;
                        }, 100);
                    },
                    willClose: () => {
                        clearInterval(timerInterval);
                    }
                }).then((result) => {
                    /* Read more about handling dismissals below */
                    if (result.dismiss === Swal.DismissReason.timer) {
                        console.log("I was closed by the timer");
                    }
                });
                setTimeout(() => window.location.reload(), 2000);
            } else {
                let timerInterval;
                Swal.fire({
                    title: "Oooops!",
                    icon: "error",
                    html: "Can not Delete this Game! [Bad Request 404]",
                    timer: 2000,
                    timerProgressBar: true,
                    didOpen: () => {
                        Swal.showLoading();
                    },
                    willClose: () => {
                        clearInterval(timerInterval);
                    }
                })
                setTimeout(() => location.reload(), 2000);
            }
        } catch (err) {
            Swal.fire({
                title: "Oooops!",
                icon: "error",
                html: "Something went so wrong!",
                timer: 4000,
                timerProgressBar: true,
                didOpen: () => {
                    Swal.showLoading();
                },
                willClose: () => {
                    clearInterval(timerInterval);
                }
            })
        }
    } else if (choice.dismiss === Swal.DismissReason.cancel) {
        swal.fire({
            title: "Cancelled",
            text: "Your file is safe :)",
            icon: "error"
        });
    }
};






btns.map((btn) => btn.addEventListener("click", deleteHandler));