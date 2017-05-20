var ViewModel = function () {
    //Make the self as 'this' reference
    var self = this;
    //Declare observable which will be bind with UI 
    self.ID = ko.observable("");
    self.Term = ko.observable("");
    self.Definition = ko.observable("");

    var glossary = {
        ID: self.ID,
        Term: self.Term,
        Definition: self.Definition
    };

    self.glossary = ko.observable();
    self.glossaries = ko.observableArray(); // Contains the list of glossary
    self.error = ko.observable();

    // Initialize the view-model
    InitData();

    function InitData() {
        $.ajax({
            // Clear error message
            url: 'api/glossaries',
            cache: false,
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            data: {},
            success: function (data) {
                self.glossaries(data); //Put the response in ObservableArray
            }
        }).fail(
            function (xhr, textStatus, err) {
                alert(err);
            });
    };

    //Add new glossary term
    self.create = function () {
        glossary.ID = 1; //should not be 0, to generate auto increment
        if (glossary.Term() != "" && glossary.Definition() != "") {
            $.ajax({
                url: 'api/glossaries',
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(glossary),
                success: function (data) {
                    self.glossaries.push(data);
                    self.ID("");
                    self.Term("");
                    self.Definition("");
                    alert("New record added successfully");
                }
            }).fail(
                function (xhr, textStatus, err) {
                    alert(err);
                });
        }
        else {
            alert('Please enter all the values !!');
        }
    }

    // Delete glossary details
    self.delete = function (glossary) {
        if (confirm('Are you sure to Delete "' + glossary.Term + '" term ??')) {
            var id = glossary.ID;
            $.ajax({
                url: 'api/glossaries/' + id,
                cache: false,
                type: 'DELETE',
                contentType: 'application/json; charset=utf-8',
                data: {},
                success: function (data) {
                    self.glossaries.remove(glossary);
                }
            }).fail(
                function (xhr, textStatus, err) {
                    alert(err);
                });
        }
    }

    // Edit glossary details
    self.edit = function (glossary) {
        self.glossary(glossary);
    }

    // Update glossary details
    self.update = function () {
        var gl = self.glossary();
        var id = gl.ID;
        $.ajax({
            url: 'api/glossaries/' + id,
            cache: false,
            type: 'PUT',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(gl),
            success: function (data) {
                self.glossaries.removeAll();
                self.glossaries(data); //Put the response in ObservableArray
                self.glossary(null);
                alert("Record updated successfully");
                InitData();
            }
        }).fail(
            function (xhr, textStatus, err) {
                alert(err);
            });
    }

    // Reset glossary details
    self.reset = function () {
        self.ID("");
        self.Term("");
        self.Definition("");
    }

    // Cancel glossary details
    self.cancel = function () {
        self.glossary(null);
    }
};

ko.applyBindings(new ViewModel());