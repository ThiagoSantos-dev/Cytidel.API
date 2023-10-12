using Cytidel.Core.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cytidel.Tests.Unit.Core.Entities
{
    public class ValidationTitleTests
    {
        private bool Act(string title) => ToDoTask.IsValidTitle(title);
        [Fact]
        public void given_valid_title_should_be_true()
        {
            var title = "Simple title";
            var result = Act(title);
            //Assert
            result.ShouldBeTrue();
        }
        [Fact]
        public void given_null_title_should_be_false()
        {
            var result = Act(null);
            //Assert
            result.ShouldBeFalse();
        }
        [Fact]
        public void given_empty_title_should_be_false()
        {
            var title = "";
            var result = Act(title);
            //Assert
            result.ShouldBeFalse();
        }
    }
}
