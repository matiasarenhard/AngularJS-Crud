var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
 
    $scope.GravarPessoa = function () {
        var Action = $("#btnSave").attr("type");
        if (Action == "submit") {
            $scope.Pessoa = {};
            $scope.Pessoa.Nome = $scope.F_Nome;
            $scope.Pessoa.Email = $scope.F_Email;
            $scope.Pessoa.Telefone = $scope.F_Telefone;
            $scope.Pessoa.Idade = $scope.F_Idade;
            $scope.Pessoa.IdPessoa = null;
            $http({
                method: "POST",
                url: "/Pessoa/GravarPessoa",
                datatype: "json",
                data:  JSON.stringify({
                        nome: $scope.F_Nome,
                        email: $scope.F_Email,
                        telefone: $scope.F_Telefone,
                        idade: $scope.F_Idade
                        })
            }).then(function (response) {
                $('#myModal').modal('toggle');
                alert(response.data);
                $scope.BuscarPessoa();
                $scope.F_Email = "";
                $scope.F_Nome = "";
                $scope.F_Telefone = "";
                $scope.F_Idade = "";
            })
        } else {
            $scope.Pessoa = {};
            $scope.Pessoa.Nome = $scope.F_Nome;
            $scope.Pessoa.Email = $scope.F_Email;
            $scope.Pessoa.Telefone = $scope.F_Telefone;
            $scope.Pessoa.Idade = $scope.F_Idade;
            $scope.Pessoa.IdPessoa = $scope.F_IdPessoa;
            $http({
                method: "post",
                url: "/Pessoa/AlterarPessoa",
                datatype: "json",
                data: JSON.stringify($scope.Pessoa)
            }).then(function (response) {
                $('#myModal').modal('toggle');
                alert(response.data);
                $scope.BuscarPessoa();
                $scope.F_Email = "";
                $scope.F_Nome = "";
                $scope.F_Telefone = "";
                $scope.F_Idade = "";
                document.getElementById("btnSave").setAttribute("value", "Submit");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
            })
        }
    }


  // Buscar
    $scope.BuscarPessoa = function () {
        $http({
            method: "get",
            url: "/Pessoa/BuscarPessoa"
        }).then(function (response) {
            $scope.pessoas = response.data;
        }, function () {
            alert("Error");
        })
    };


    //Deletar
    $scope.DeletePessoa = function (Obj) {
        $http({
            method: "post",
            url: "/Pessoa/DeletePessoa",
            datatype: "json",
            data: JSON.stringify(Obj)
        }).then(function (response) {
            alert(response.data);
            $scope.BuscarPessoa();
        })
    };



    //Update
    $scope.UpdatePessoa = function (Obj) {
        $('#myModal').modal('show');
        $scope.F_Nome = Obj.Nome;
        $scope.F_Email = Obj.Email;
        $scope.F_Telefone = Obj.Telefone;
        $scope.F_Idade = Obj.Idade;
        $scope.F_IdPessoa = Obj.IdPessoa;
        $("#btnSave").attr("type","Update");
        document.getElementById("btnSave").style.backgroundColor = "Yellow";
    }
})