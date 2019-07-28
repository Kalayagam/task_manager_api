using System;
using Xunit;
using Moq;
using TaskManager.Business;
using System.Collections.Generic;
using TaskManager.Core;
using TaskManagerApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Interfaces;

namespace TaskManager.Test
{
    public class TaskManagerControllerTest
    {
        private readonly Mock<IBusiness<TaskViewModel>> _mockTaskManager;
        private readonly TaskManagerController _sut;

        public TaskManagerControllerTest()
        {
            _mockTaskManager = new Mock<IBusiness<TaskViewModel>>();
            _sut = new TaskManagerController(_mockTaskManager.Object);
        }

        [Fact]
        public async void GetAllTasksTest()
        {
            //Act
            var mockTaskViewModel = new List<TaskViewModel>() {
                new TaskViewModel()
                {
                   Id = 1,
                   TaskName = "Task Name",
                   EndDate = new DateTime(),
                   StartDate = new DateTime(),
                   ParentId = 0,
                   ParentTaskName = null,
                   Priority = 5
                }
            };

            _mockTaskManager.Setup(x => x.GetAll()).ReturnsAsync(mockTaskViewModel);

            //Action
            var response = await _sut.GetAllTasks();

            //Assert
            var oKObjectResult = Assert.IsType<OkObjectResult>(response);
            var viewModel = Assert.IsAssignableFrom<List<TaskViewModel>>(oKObjectResult.Value);
            Assert.Equal(mockTaskViewModel[0].Id, viewModel[0].Id);
        }
        [Fact]
        public async void GetTaskTest()
        {
            var mockTaskViewModel =
            new TaskViewModel()
            {
                Id = 1
            };
            _mockTaskManager.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(mockTaskViewModel);
            //Action
            var response = await _sut.Get(mockTaskViewModel.Id);

            //Assert
            var oKObjectResult = Assert.IsType<OkObjectResult>(response);
            var viewModel = Assert.IsAssignableFrom<TaskViewModel>(oKObjectResult.Value);
            Assert.Equal(mockTaskViewModel.Id, viewModel.Id);
        }            
    }
}
