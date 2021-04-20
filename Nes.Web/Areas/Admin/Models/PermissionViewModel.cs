using Nes.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nes.Web.Areas.Admin.Models
{
    public class PermissionViewModel
    {
        public IEnumerable<Function> AvailableFunctions { get; set; }
        public IEnumerable<Function> SelectedFunctions { get; set; }
        public PostedFunctionIds PostedFunctions { get; set; }

        public IEnumerable<Role> AvailableRoles { get; set; }
        public IEnumerable<Role> SelectedRoles { get; set; }
        public PostedRoleIds PostedRoles { get; set; }

        public UserGroup UserGroup { get; set; }
    }
    public class PostedFunctionIds
    {
        //this array will be used to POST values from the form to the controller
        public string[] FunctionIds { get; set; }
    }
    public class PostedRoleIds
    {
        //this array will be used to POST values from the form to the controller
        public string[] RoleIds { get; set; }
    }
}