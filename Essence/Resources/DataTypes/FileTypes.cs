using System;
using System.Collections.Generic;
using System.Text;

namespace Essence.Resources.DataTypes
{
  public class Settings
  {
    public string Token { get; set; }
    public string Playing { get; set; }
    public string Prefix { get; set; }
    public ulong DeleteDelay { get; set; }
  }
}
