using System;
using System.Collections.Generic;

namespace Onianov.Models;

public partial class TypeUser
{
    public int TypeUserId { get; set; }

    public string? TypeUserName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
