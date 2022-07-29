

namespace jwtapi
{
       public class ClaimProperty
       {
              public ClaimProperty(string type, string value)
              {
                     this.Type = type;
                     this.Value = value;

              }
              public string Type { get; set; }
              public string Value { get; set; }

       }
}