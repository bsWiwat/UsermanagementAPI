using System;
using static CodePluse.API.DTO.Reponse.ResponseDTO;

namespace CodePluse.API.DTO
{
	public class PermissionDTO
	{
        public Status status { get; set; }

        public PermissionData data { get; set; }
    }
}

