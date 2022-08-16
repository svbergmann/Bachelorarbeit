public class DataNotFoundException : Exception {
        public HttpStatusCode StatusCode = HttpStatusCode.NotFound;

        public DataNotFoundException() : base("Requested data was not found.") { }
}