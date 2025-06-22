$(document).ready(function () {
    loadAnimals();

    $('#animalForm').on('submit', function (e) {
        e.preventDefault();
        let formData = new FormData(this);
        let id = $('#animalId').val();
        let url = id ? '/Animal/UpdateAnimal' : '/Animal/AddAnimal';

        $.ajax({
            url: url,
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function () {
                loadAnimals();
                $('#animalForm')[0].reset();
                $('#animalId').val('');
            }
        });
    });
});

function loadAnimals() {
    $.get('/Animal/GetAnimals', function (data) {
        let rows = '';
        data.forEach(item => {
            rows += `
                <tr>
                    <td>${item.Name}</td>
                    <td>${item.Category}</td>
                    <td>${item.Description}</td>
                    <td><img src="${item.ImagePath}" width="50" /></td>
                    <td>
                        <button class="btn btn-sm btn-warning" onclick="editAnimal(${item.Id}, '${item.Name}', '${item.Category}', '${item.Description}')">Edit</button>
                        <button class="btn btn-sm btn-danger" onclick="deleteAnimal(${item.Id})">Delete</button>
                    </td>
                </tr>`;
        });
        $('#animalTable').html(rows);
    });
}

function editAnimal(id, name, category, description) {
    $('#animalId').val(id);
    $('#name').val(name);
    $('#category').val(category);
    $('#description').val(description);
}

function deleteAnimal(id) {
    if (confirm("Are you sure to delete?")) {
        $.post('/Animal/DeleteAnimal', { id: id }, function () {
            loadAnimals();
        });
    }
}
