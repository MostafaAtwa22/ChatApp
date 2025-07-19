var createGroupBtn = document.getElementById('create-group-btn');
var createGroupModal = document.getElementById('create-group-modal');
createGroupBtn.addEventListener('click', function () {
    createGroupModal.classList.add('active')
})

function closeModal() {
    document.getElementById('create-group-modal').classList.remove('active');
}