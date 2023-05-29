using EasyInvoice.API.Shared.ViewModelError;

namespace EasyInvoice.API.Shared.Responses
{
    public static class Responses
    {
        public static ResultViewModelError ApplicationErrorMessage()
        {
            return new ResultViewModelError
            {
                Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente.",
                Success = false,
                Data = null
            };
        }

        public static ResultViewModelError DomainErrorMessage(string message)
        {
            return new ResultViewModelError
            {
                Message = message,
                Success = false,
                Data = null
            };
        }

        public static ResultViewModelError DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
        {
            return new ResultViewModelError
            {
                Message = message,
                Success = false,
                Data = null
            };
        }

        public static ResultViewModelError UnauthorizedErrorMessage()
        {
            return new ResultViewModelError
            {
                Message = "Login e ou senha incorretos.",
                Success = false,
                Data = null
            };
        }
    }
}
