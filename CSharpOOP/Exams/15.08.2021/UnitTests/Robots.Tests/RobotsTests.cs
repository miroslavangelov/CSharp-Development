using NUnit.Framework;

namespace Robots.Tests
{
    using System;

    [TestFixture]
    public class RobotsTests
    {
        [Test]
        public void ConstructorShouldSetCapacityCorrectly()
        {
            RobotManager robotManager = new RobotManager(6);
            
            Assert.That(robotManager.Capacity, Is.EqualTo(6));
        }

        [Test]
        public void NegativeCapacityShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new RobotManager(-1);
            }, "Invalid capacity!");
        }

        [Test]
        public void ShouldSetCountCorrectly()
        {
            RobotManager robotManager = new RobotManager(6);
            Robot robot = new Robot("Robot", 5);
            
            robotManager.Add(robot);
            
            Assert.That(robotManager.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddMethodShouldThrowExceptionIfRobotAlreadyExist()
        {
            RobotManager robotManager = new RobotManager(6);
            Robot robot = new Robot("Robot", 5);
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(robot);
            }, $"There is already a robot with name {robot.Name}!");
        }
        
        [Test]
        public void AddMethodShouldThrowExceptionIfCapacityIsFull()
        {
            RobotManager robotManager = new RobotManager(1);
            Robot robot = new Robot("Robot", 5);
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(robot);
            }, "Not enough capacity!");
        }

        [Test]
        public void RemoveMethodShouldWorkCorrectly()
        {
            RobotManager robotManager = new RobotManager(1);
            Robot robot = new Robot("Robot", 5);
            robotManager.Add(robot);
            robotManager.Remove(robot.Name);
            
            Assert.That(robotManager.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveMethodShouldThrowExceptionIfRobotIsNotFound()
        {
            RobotManager robotManager = new RobotManager(1);
            Robot robot = new Robot("Robot", 5);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Remove(robot.Name);
            }, $"Robot with the name {robot.Name} doesn't exist!");
        }

        [Test]
        public void WorkMethodShouldWorkCorrectly()
        {
            RobotManager robotManager = new RobotManager(1);
            Robot robot = new Robot("Robot", 5);
            robotManager.Add(robot);
            robotManager.Work(robot.Name, "walk", 3);
            
            Assert.That(robot.Battery, Is.EqualTo(2));
        }
        
        [Test]
        public void WorkMethodShouldThrowExceptionIfRobotIsNotFound()
        {
            RobotManager robotManager = new RobotManager(1);
            Robot robot = new Robot("Robot", 5);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work(robot.Name, "walk", 3);
            }, $"Robot with the name {robot.Name} doesn't exist!");
        }
        
        [Test]
        public void WorkMethodShouldThrowExceptionIfBatteryIsNotEnough()
        {
            RobotManager robotManager = new RobotManager(1);
            Robot robot = new Robot("Robot", 5);
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work(robot.Name, "walk", 6);
            }, $"{robot.Name} doesn't have enough battery!");
        }

        [Test]
        public void MethodChargeShouldWorkCorrectly()
        {
            RobotManager robotManager = new RobotManager(1);
            Robot robot = new Robot("Robot", 5);
            robotManager.Add(robot);
            robotManager.Work(robot.Name, "walk", 5);
            robotManager.Charge(robot.Name);

            Assert.That(robot.Battery, Is.EqualTo(5));
        }
        
        [Test]
        public void MethodChargeShouldThrowExceptionIfRobotIsNotFound()
        {
            RobotManager robotManager = new RobotManager(1);
            Robot robot = new Robot("Robot", 5);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Charge(robot.Name);
            }, $"Robot with the name {robot.Name} doesn't exist!");
        }
    }
}
