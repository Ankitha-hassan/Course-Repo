namespace CourseService.Domain.DTO
{
    public class WebAPIResult<T>
    {
        public T Result { get; set; }
        public WebAPIErrorMessage ErrorMessage { get; set; } = new WebAPIErrorMessage();
    }
}
