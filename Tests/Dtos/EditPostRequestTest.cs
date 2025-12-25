using BoardApi.Dtos;
using Tests.Utils;

namespace Tests.Dtos
{
    public class EditPostRequestTest
    {
        [Fact]
        public void Validate_제목과내용이null이면_실패()
        {
            var request = new EditPostRequest(null, null);
            var result = ModelValidateHelper.Validate(request);
            
            Assert.Contains(result, v => v.ErrorMessage == "At least one of Title or Content must be provided.");
        }
    }
}