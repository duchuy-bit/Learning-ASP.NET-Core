namespace ShopManager.Helper
{
    public class ImageHelper
    {
        public static string UpLoadImage(IFormFile Hinh, string folder)
        {
            try
            {
                //Tạo tên file theo thời gian upload -> tránh trùng tên file
                var FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Hinh.FileName;

                //Upload Image vào wwwroot, folder Images
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", folder, FileName);
                using (var myFile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Hinh.CopyTo(myFile);
                }
                return FileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }

    }
}
