using AutoMapper;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Data;
using System.Data.SqlClient;
using Squib.UserService.API.Model;
using Squib.UserService.API.Repository;
using Squib.UserService.API.Service;
using Xunit;
using Squib.UserService.API.model;
using Newtonsoft.Json;

namespace TestProject1
{
    public class UnitTest1
    {
        private readonly Mock<IUserRepo> _userRepository;
        private readonly UserServi _userServi;
         private readonly IMapper _mapper;
       

        public UnitTest1()
        {
            _userRepository = new Mock<IUserRepo>();
            
            // Setup AutoMapper with UserDto to UserRDto mapping
        var config = new MapperConfiguration(cfg => {
            cfg.CreateMap<UserDto, UserRDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        });
        _mapper = config.CreateMapper();
        _userServi = new UserServi(_userRepository.Object, _mapper);
        }
           

[Fact]
    public void GetUsers_ShouldReturnMappedAndSerializedUsers()
    {
        // Arrange
        var users = new List<UserDto>
        {
            new UserDto{ Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" },
            new UserDto{ Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane@example.com" }
        };

        _userRepository.Setup(repo => repo.GetUsers()).Returns(users);

        // Act
        var result = _userServi.GetUsers();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        // Ensure the FullName and Email are correctly mapped
        Assert.Equal("John Doe", result[0].FullName);
        Assert.Equal("john@example.com", result[0].Email);

        Assert.Equal("Jane Doe", result[1].FullName);
        Assert.Equal("jane@example.com", result[1].Email);

        // Serialize and Deserialize check
        string jsonString = JsonConvert.SerializeObject(result);
        var deserializedUserDto = JsonConvert.DeserializeObject<List<UserRDto>>(jsonString);

        // Ensure the deserialized data matches the mapped UserRDto objects
        Assert.Equal(result[0].FullName, deserializedUserDto[0].FullName);
        Assert.Equal(result[0].Email, deserializedUserDto[0].Email);

        Assert.Equal(result[1].FullName, deserializedUserDto[1].FullName);
        Assert.Equal(result[1].Email, deserializedUserDto[1].Email);
    }
        [Fact]
        public void GetUserByIdTest()
        {
            // Arrange
            var user = new UserDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };

            _userRepository.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(user);

            var result = _userServi.GetUserById(1);

            // Assert
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
        }

        [Fact]
        public void UpdateUser_SuccessfulUpdate_ReturnsTrue()
        {
            // Arrange
            var user = new UserDto
            {
                Id = 1,
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe"
            };

            _userRepository.Setup(x => x.UpdateUser(It.Is<UserDto>(u => u.Id == user.Id))).Returns(true);

            // Act
            bool result = _userServi.UpdateUser(user);

            // Assert
            Assert.True(result); // Assuming the update was successful
        }

        [Fact]
        public void DeleteUser_SuccessfulDeletion_ReturnsTrue()
        {
            // Arrange
            _userRepository.Setup(x => x.DeleteUser(It.IsAny<int>())).Returns(true);
            // Act
            bool result = _userServi.DeleteUser(1);
            // Assert
            Assert.True(result); // Assuming the deletion was successful
            _userRepository.Verify(x => x.DeleteUser(1), Times.Once);
            _userRepository.VerifyNoOtherCalls();
            _userRepository.Reset();
        }
        
        [Fact]
        public void AddUser_SuccessfulAddition_ReturnsTrue()
        {
            // Arrange
            var user = new UserDto
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe"
            };
            _userRepository.Setup(x => x.AddUser(It.Is<UserDto>(u => u.Email == user.Email))).Returns(true);
            // Act
            bool result = _userServi.AddUser(user);
            // Assert
            Assert.True(result); // Assuming the addition was successful
            _userRepository.Verify(x => x.AddUser(user), Times.Once);
            _userRepository.VerifyNoOtherCalls();
            _userRepository.Reset();
        }
    }
}