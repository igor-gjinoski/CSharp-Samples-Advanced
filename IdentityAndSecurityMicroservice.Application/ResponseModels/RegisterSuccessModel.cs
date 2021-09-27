using System;

namespace IdentityAndSecurityMicroservice.Application.ResponseModels
{
    public class RegisterSuccessModel
    {
        public RegisterSuccessModel(Guid id)
            => Id = id;

        public Guid Id { get; set; }
    }
}
