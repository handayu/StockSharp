namespace StockSharp.Messages
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Net;
	using System.Runtime.Serialization;
	using System.Security;
	using System.Xml.Serialization;

	using Ecng.Common;
	using Ecng.Collections;

	using StockSharp.Localization;

	/// <summary>
	/// The message contains information about user.
	/// </summary>
	[DataContract]
	[Serializable]
	public class UserInfoMessage : BaseSubscriptionIdMessage<UserInfoMessage>, ITransactionIdMessage
	{
		/// <summary>
		/// Login.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.LoginKey,
			Description = LocalizedStrings.LoginKey + LocalizedStrings.Dot,
			GroupName = LocalizedStrings.GeneralKey,
			Order = 0)]
		public string Login { get; set; }

		[field: NonSerialized]
		private SecureString _password;

		/// <summary>
		/// Portfolio currency.
		/// </summary>
		[DataMember]
		[Display(
			ResourceType = typeof(LocalizedStrings),
			Name = LocalizedStrings.PasswordKey,
			Description = LocalizedStrings.PasswordKey + LocalizedStrings.Dot,
			GroupName = LocalizedStrings.GeneralKey,
			Order = 1)]
		public SecureString Password
		{
			get => _password;
			set => _password = value;
		}

		/// <inheritdoc />
		[DataMember]
		public long TransactionId { get; set; }

		/// <summary>
		/// Is blocked.
		/// </summary>
		[DataMember]
		public bool IsBlocked { get; set; }

		/// <summary>
		/// Identifier.
		/// </summary>
		[DataMember]
		public long? Id { get; set; }

		/// <summary>
		/// Display name.
		/// </summary>
		[DataMember]
		public string DisplayName { get; set; }

		/// <summary>
		/// Phone.
		/// </summary>
		[DataMember]
		public string Phone { get; set; }

		/// <summary>
		/// Web site.
		/// </summary>
		[DataMember]
		public string Homepage { get; set; }

		/// <summary>
		/// Skype.
		/// </summary>
		[DataMember]
		public string Skype { get; set; }

		/// <summary>
		/// City.
		/// </summary>
		[DataMember]
		public string City { get; set; }

		/// <summary>
		/// Gender.
		/// </summary>
		[DataMember]
		public bool? Gender { get; set; }

		/// <summary>
		/// Is the mail-out enabled.
		/// </summary>
		[DataMember]
		public bool? IsSubscription { get; set; }

		/// <summary>
		/// Language.
		/// </summary>
		[DataMember]
		public string Language { get; set; }

		/// <summary>
		/// Balance.
		/// </summary>
		[DataMember]
		public decimal? Balance { get; set; }

		/// <summary>
		/// Balance.
		/// </summary>
		[DataMember]
		public long? Avatar { get; set; }

		/// <summary>
		/// Token.
		/// </summary>
		[DataMember]
		public string AuthToken { get; set; }

		/// <summary>
		/// Date of registration.
		/// </summary>
		[DataMember]
		public DateTimeOffset? CreationDate { get; set; }

		private IEnumerable<IPAddress> _ipRestrictions = Enumerable.Empty<IPAddress>();

		/// <summary>
		/// IP address restrictions.
		/// </summary>
		[XmlIgnore]
		public IEnumerable<IPAddress> IpRestrictions
		{
			get => _ipRestrictions;
			set => _ipRestrictions = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Permission set.
		/// </summary>
		public IDictionary<UserPermissions, IDictionary<Tuple<string, string, object, DateTime?>, bool>> Permissions { get; } = new Dictionary<UserPermissions, IDictionary<Tuple<string, string, object, DateTime?>, bool>>();

		/// <summary>
		/// Can publish NuGet packages.
		/// </summary>
		[DataMember]
		public bool CanPublish { get; set; }

		/// <summary>
		/// Is EULA accepted.
		/// </summary>
		[DataMember]
		public bool? IsAgreementAccepted { get; set; }

		/// <inheritdoc />
		public override DataType DataType => DataType.Users;

		/// <summary>
		/// Upload size limit.
		/// </summary>
		[DataMember]
		public long UploadLimit { get; set; }

		private string[] _features = Array.Empty<string>();

		/// <summary>
		/// Available features.
		/// </summary>
		[DataMember]
		public string[] Features
		{
			get => _features;
			set => _features = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Is trial verified.
		/// </summary>
		[DataMember]
		public bool IsTrialVerified { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="UserInfoMessage"/>.
		/// </summary>
		public UserInfoMessage()
			: base(MessageTypes.UserInfo)
		{
		}

		/// <inheritdoc />
		public override string ToString() => base.ToString() + $",Name={Login}";

		/// <inheritdoc />
		public override void CopyTo(UserInfoMessage destination)
		{
			base.CopyTo(destination);

			destination.Login = Login;
			destination.Password = Password;
			destination.TransactionId = TransactionId;
			destination.OriginalTransactionId = OriginalTransactionId;
			destination.IsBlocked = IsBlocked;
			destination.IpRestrictions = IpRestrictions?.ToArray() ?? Enumerable.Empty<IPAddress>();
			destination.Id = Id;
			destination.DisplayName = DisplayName;
			destination.Phone = Phone;
			destination.Homepage = Homepage;
			destination.Skype = Skype;
			destination.City = City;
			destination.Gender = Gender;
			destination.IsSubscription = IsSubscription;
			destination.Language = Language;
			destination.Balance = Balance;
			destination.Avatar = Avatar;
			destination.CreationDate = CreationDate;
			destination.AuthToken = AuthToken;
			destination.CanPublish = CanPublish;
			destination.IsAgreementAccepted = IsAgreementAccepted;
			destination.UploadLimit = UploadLimit;

			if (Features.Length > 0)
				destination.Features = Features.ToArray();

			if (Permissions?.Count > 0)
				destination.Permissions.AddRange(Permissions.ToDictionary());

			destination.IsTrialVerified = IsTrialVerified;
		}
	}
}