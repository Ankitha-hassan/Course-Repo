namespace CourseService.Constant
{
    public static class RouteMapConstants
    {
     
        public const string BaseRoute = "api/v1";
        public const string BaseControllerRoute = BaseRoute + "/[controller]";
        
        public const string GetCourseById = "{courseId}";
        public const string UpdateCourseById = "{courseId}";
        public const string DeleteCourseById = "{courseId}";
        public const string GetAllTopicsByCourseId = "{courseId}/topics";
        public const string GetTopicById = "topics/{topicId}";
        public const string AddTopic = "topics";
        public const string UpdateTopicById = "topics/{topicId}";
        public const string DeleteTopicById = "topics/{topicId}";
        public const string GetSubTopicsByTopicId = "topics/{topicId}/subtopics";
        public const string GetSubTopicById = "subtopics/{subTopicId}";
        public const string AddSubTopicByTopicID = "topics/subtopics";
        public const string UpdateSubTopicById = "subtopics/{subTopicId}";
        public const string DeleteSubTopicById = "subtopics/{subTopicId}";


    }
}
