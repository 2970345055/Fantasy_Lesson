using ProtoBuf;
using Unity.Mathematics;
using System.Collections.Generic;
using Fantasy.Core.Network;
#pragma warning disable CS8618

namespace Fantasy
{	
	/// <summary>
	///  Gate跟Map服务器进行通讯、注册Address协议
	/// </summary>
	[ProtoContract]
	public partial class I_G2M_LoginAddressRequest : AProto, IRouteRequest
	{
		[ProtoIgnore]
		public I_M2G_LoginAddressResponse ResponseType { get; set; }
		public uint OpCode() { return InnerOpcode.I_G2M_LoginAddressRequest; }
		public long RouteTypeOpCode() { return CoreRouteType.Route; }
		[ProtoMember(1)]
		public long AddressableId { get; set; }
		[ProtoMember(2)]
		public long GateRouteId { get; set; }
	}
	[ProtoContract]
	public partial class I_M2G_LoginAddressResponse : AProto, IRouteResponse
	{
		public uint OpCode() { return InnerOpcode.I_M2G_LoginAddressResponse; }
		[ProtoMember(91, IsRequired = true)]
		public uint ErrorCode { get; set; }
		[ProtoMember(1)]
		public long AddressableId { get; set; }
	}
	/// <summary>
	/// gate 和Chat 服务器进程通讯 请求消息
	/// </summary>
	[ProtoContract]
	public partial class I_G2C_MessageRequest : AProto, IRouteRequest
	{
		[ProtoIgnore]
		public I_G2C_MessageResponse ResponseType { get; set; }
		public uint OpCode() { return InnerOpcode.I_G2C_MessageRequest; }
		public long RouteTypeOpCode() { return CoreRouteType.Route; }
		[ProtoMember(1)]
		public long GateRouteId { get; set; }
		[ProtoMember(2)]
		public string Name { get; set; }
	}
	[ProtoContract]
	public partial class I_G2C_MessageResponse : AProto, IRouteResponse
	{
		public uint OpCode() { return InnerOpcode.I_G2C_MessageResponse; }
		[ProtoMember(91, IsRequired = true)]
		public uint ErrorCode { get; set; }
		[ProtoMember(1)]
		public long ChatRouteId { get; set; }
	}
}
