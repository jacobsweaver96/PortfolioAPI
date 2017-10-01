﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortfolioAPI.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PortfolioDB")]
	public partial class PortfolioDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertApiPermission(ApiPermission instance);
    partial void UpdateApiPermission(ApiPermission instance);
    partial void DeleteApiPermission(ApiPermission instance);
    partial void InsertClient(Client instance);
    partial void UpdateClient(Client instance);
    partial void DeleteClient(Client instance);
    partial void InsertGithubUser(GithubUser instance);
    partial void UpdateGithubUser(GithubUser instance);
    partial void DeleteGithubUser(GithubUser instance);
    partial void InsertRole(Role instance);
    partial void UpdateRole(Role instance);
    partial void DeleteRole(Role instance);
    partial void InsertUserRole_Map(UserRole_Map instance);
    partial void UpdateUserRole_Map(UserRole_Map instance);
    partial void DeleteUserRole_Map(UserRole_Map instance);
    #endregion
		
		public PortfolioDBDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["PortfolioDBConnectionString1"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public PortfolioDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PortfolioDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PortfolioDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PortfolioDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public System.Data.Linq.Table<ApiPermission> ApiPermissions
		{
			get
			{
				return this.GetTable<ApiPermission>();
			}
		}
		
		public System.Data.Linq.Table<Client> Clients
		{
			get
			{
				return this.GetTable<Client>();
			}
		}
		
		public System.Data.Linq.Table<GithubUser> GithubUsers
		{
			get
			{
				return this.GetTable<GithubUser>();
			}
		}
		
		public System.Data.Linq.Table<Role> Roles
		{
			get
			{
				return this.GetTable<Role>();
			}
		}
		
		public System.Data.Linq.Table<UserRole_Map> UserRole_Maps
		{
			get
			{
				return this.GetTable<UserRole_Map>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserId;
		
		private string _Email;
		
		private string _Password;
		
		private string _Salt;
		
		private bool _IsDeleted;
		
		private EntitySet<GithubUser> _GithubUsers;
		
		private EntitySet<UserRole_Map> _UserRole_Maps;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    partial void OnSaltChanging(string value);
    partial void OnSaltChanged();
    partial void OnIsDeletedChanging(bool value);
    partial void OnIsDeletedChanged();
    #endregion
		
		public User()
		{
			this._GithubUsers = new EntitySet<GithubUser>(new Action<GithubUser>(this.attach_GithubUsers), new Action<GithubUser>(this.detach_GithubUsers));
			this._UserRole_Maps = new EntitySet<UserRole_Map>(new Action<UserRole_Map>(this.attach_UserRole_Maps), new Action<UserRole_Map>(this.detach_UserRole_Maps));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="NVarChar(200) NOT NULL", CanBeNull=false)]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Salt", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Salt
		{
			get
			{
				return this._Salt;
			}
			set
			{
				if ((this._Salt != value))
				{
					this.OnSaltChanging(value);
					this.SendPropertyChanging();
					this._Salt = value;
					this.SendPropertyChanged("Salt");
					this.OnSaltChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDeleted", DbType="Bit NOT NULL")]
		public bool IsDeleted
		{
			get
			{
				return this._IsDeleted;
			}
			set
			{
				if ((this._IsDeleted != value))
				{
					this.OnIsDeletedChanging(value);
					this.SendPropertyChanging();
					this._IsDeleted = value;
					this.SendPropertyChanged("IsDeleted");
					this.OnIsDeletedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_GithubUser", Storage="_GithubUsers", ThisKey="UserId", OtherKey="UserId")]
		public EntitySet<GithubUser> GithubUsers
		{
			get
			{
				return this._GithubUsers;
			}
			set
			{
				this._GithubUsers.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserRole_Map", Storage="_UserRole_Maps", ThisKey="UserId", OtherKey="UserId")]
		public EntitySet<UserRole_Map> UserRole_Maps
		{
			get
			{
				return this._UserRole_Maps;
			}
			set
			{
				this._UserRole_Maps.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_GithubUsers(GithubUser entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_GithubUsers(GithubUser entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		
		private void attach_UserRole_Maps(UserRole_Map entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_UserRole_Maps(UserRole_Map entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ApiPermissions")]
	public partial class ApiPermission : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _PermissionName;
		
		private System.Nullable<bool> _CanRead;
		
		private System.Nullable<bool> _CanWrite;
		
		private System.Nullable<bool> _CanAlterPermissions;
		
		private int _ApiPermissionsId;
		
		private EntitySet<Client> _Clients;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPermissionNameChanging(string value);
    partial void OnPermissionNameChanged();
    partial void OnCanReadChanging(System.Nullable<bool> value);
    partial void OnCanReadChanged();
    partial void OnCanWriteChanging(System.Nullable<bool> value);
    partial void OnCanWriteChanged();
    partial void OnCanAlterPermissionsChanging(System.Nullable<bool> value);
    partial void OnCanAlterPermissionsChanged();
    partial void OnApiPermissionsIdChanging(int value);
    partial void OnApiPermissionsIdChanged();
    #endregion
		
		public ApiPermission()
		{
			this._Clients = new EntitySet<Client>(new Action<Client>(this.attach_Clients), new Action<Client>(this.detach_Clients));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermissionName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string PermissionName
		{
			get
			{
				return this._PermissionName;
			}
			set
			{
				if ((this._PermissionName != value))
				{
					this.OnPermissionNameChanging(value);
					this.SendPropertyChanging();
					this._PermissionName = value;
					this.SendPropertyChanged("PermissionName");
					this.OnPermissionNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CanRead", DbType="Bit")]
		public System.Nullable<bool> CanRead
		{
			get
			{
				return this._CanRead;
			}
			set
			{
				if ((this._CanRead != value))
				{
					this.OnCanReadChanging(value);
					this.SendPropertyChanging();
					this._CanRead = value;
					this.SendPropertyChanged("CanRead");
					this.OnCanReadChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CanWrite", DbType="Bit")]
		public System.Nullable<bool> CanWrite
		{
			get
			{
				return this._CanWrite;
			}
			set
			{
				if ((this._CanWrite != value))
				{
					this.OnCanWriteChanging(value);
					this.SendPropertyChanging();
					this._CanWrite = value;
					this.SendPropertyChanged("CanWrite");
					this.OnCanWriteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CanAlterPermissions", DbType="Bit")]
		public System.Nullable<bool> CanAlterPermissions
		{
			get
			{
				return this._CanAlterPermissions;
			}
			set
			{
				if ((this._CanAlterPermissions != value))
				{
					this.OnCanAlterPermissionsChanging(value);
					this.SendPropertyChanging();
					this._CanAlterPermissions = value;
					this.SendPropertyChanged("CanAlterPermissions");
					this.OnCanAlterPermissionsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ApiPermissionsId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ApiPermissionsId
		{
			get
			{
				return this._ApiPermissionsId;
			}
			set
			{
				if ((this._ApiPermissionsId != value))
				{
					this.OnApiPermissionsIdChanging(value);
					this.SendPropertyChanging();
					this._ApiPermissionsId = value;
					this.SendPropertyChanged("ApiPermissionsId");
					this.OnApiPermissionsIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ApiPermission_Client", Storage="_Clients", ThisKey="ApiPermissionsId", OtherKey="PermissionsId")]
		public EntitySet<Client> Clients
		{
			get
			{
				return this._Clients;
			}
			set
			{
				this._Clients.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Clients(Client entity)
		{
			this.SendPropertyChanging();
			entity.ApiPermission = this;
		}
		
		private void detach_Clients(Client entity)
		{
			this.SendPropertyChanging();
			entity.ApiPermission = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Clients")]
	public partial class Client : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _ClientName;
		
		private bool _IsDeleted;
		
		private string _ClientKey;
		
		private int _ClientId;
		
		private int _PermissionsId;
		
		private EntityRef<ApiPermission> _ApiPermission;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnClientNameChanging(string value);
    partial void OnClientNameChanged();
    partial void OnIsDeletedChanging(bool value);
    partial void OnIsDeletedChanged();
    partial void OnClientKeyChanging(string value);
    partial void OnClientKeyChanged();
    partial void OnClientIdChanging(int value);
    partial void OnClientIdChanged();
    partial void OnPermissionsIdChanging(int value);
    partial void OnPermissionsIdChanged();
    #endregion
		
		public Client()
		{
			this._ApiPermission = default(EntityRef<ApiPermission>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClientName", DbType="NVarChar(200) NOT NULL", CanBeNull=false)]
		public string ClientName
		{
			get
			{
				return this._ClientName;
			}
			set
			{
				if ((this._ClientName != value))
				{
					this.OnClientNameChanging(value);
					this.SendPropertyChanging();
					this._ClientName = value;
					this.SendPropertyChanged("ClientName");
					this.OnClientNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDeleted", DbType="Bit NOT NULL")]
		public bool IsDeleted
		{
			get
			{
				return this._IsDeleted;
			}
			set
			{
				if ((this._IsDeleted != value))
				{
					this.OnIsDeletedChanging(value);
					this.SendPropertyChanging();
					this._IsDeleted = value;
					this.SendPropertyChanged("IsDeleted");
					this.OnIsDeletedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClientKey", DbType="NVarChar(32)")]
		public string ClientKey
		{
			get
			{
				return this._ClientKey;
			}
			set
			{
				if ((this._ClientKey != value))
				{
					this.OnClientKeyChanging(value);
					this.SendPropertyChanging();
					this._ClientKey = value;
					this.SendPropertyChanged("ClientKey");
					this.OnClientKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClientId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ClientId
		{
			get
			{
				return this._ClientId;
			}
			set
			{
				if ((this._ClientId != value))
				{
					this.OnClientIdChanging(value);
					this.SendPropertyChanging();
					this._ClientId = value;
					this.SendPropertyChanged("ClientId");
					this.OnClientIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermissionsId", DbType="Int NOT NULL")]
		public int PermissionsId
		{
			get
			{
				return this._PermissionsId;
			}
			set
			{
				if ((this._PermissionsId != value))
				{
					if (this._ApiPermission.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPermissionsIdChanging(value);
					this.SendPropertyChanging();
					this._PermissionsId = value;
					this.SendPropertyChanged("PermissionsId");
					this.OnPermissionsIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ApiPermission_Client", Storage="_ApiPermission", ThisKey="PermissionsId", OtherKey="ApiPermissionsId", IsForeignKey=true)]
		public ApiPermission ApiPermission
		{
			get
			{
				return this._ApiPermission.Entity;
			}
			set
			{
				ApiPermission previousValue = this._ApiPermission.Entity;
				if (((previousValue != value) 
							|| (this._ApiPermission.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ApiPermission.Entity = null;
						previousValue.Clients.Remove(this);
					}
					this._ApiPermission.Entity = value;
					if ((value != null))
					{
						value.Clients.Add(this);
						this._PermissionsId = value.ApiPermissionsId;
					}
					else
					{
						this._PermissionsId = default(int);
					}
					this.SendPropertyChanged("ApiPermission");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GithubUsers")]
	public partial class GithubUser : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _GithubUserId;
		
		private int _UserId;
		
		private string _Username;
		
		private bool _IsDeleted;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnGithubUserIdChanging(int value);
    partial void OnGithubUserIdChanged();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnUsernameChanging(string value);
    partial void OnUsernameChanged();
    partial void OnIsDeletedChanging(bool value);
    partial void OnIsDeletedChanged();
    #endregion
		
		public GithubUser()
		{
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GithubUserId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int GithubUserId
		{
			get
			{
				return this._GithubUserId;
			}
			set
			{
				if ((this._GithubUserId != value))
				{
					this.OnGithubUserIdChanging(value);
					this.SendPropertyChanging();
					this._GithubUserId = value;
					this.SendPropertyChanged("GithubUserId");
					this.OnGithubUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Username", DbType="NVarChar(200) NOT NULL", CanBeNull=false)]
		public string Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				if ((this._Username != value))
				{
					this.OnUsernameChanging(value);
					this.SendPropertyChanging();
					this._Username = value;
					this.SendPropertyChanged("Username");
					this.OnUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDeleted", DbType="Bit NOT NULL")]
		public bool IsDeleted
		{
			get
			{
				return this._IsDeleted;
			}
			set
			{
				if ((this._IsDeleted != value))
				{
					this.OnIsDeletedChanging(value);
					this.SendPropertyChanging();
					this._IsDeleted = value;
					this.SendPropertyChanged("IsDeleted");
					this.OnIsDeletedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_GithubUser", Storage="_User", ThisKey="UserId", OtherKey="UserId", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.GithubUsers.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.GithubUsers.Add(this);
						this._UserId = value.UserId;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Roles")]
	public partial class Role : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _RoleId;
		
		private string _Name;
		
		private string _Description;
		
		private bool _IsDeleted;
		
		private EntitySet<UserRole_Map> _UserRole_Maps;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRoleIdChanging(int value);
    partial void OnRoleIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnIsDeletedChanging(bool value);
    partial void OnIsDeletedChanged();
    #endregion
		
		public Role()
		{
			this._UserRole_Maps = new EntitySet<UserRole_Map>(new Action<UserRole_Map>(this.attach_UserRole_Maps), new Action<UserRole_Map>(this.detach_UserRole_Maps));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoleId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int RoleId
		{
			get
			{
				return this._RoleId;
			}
			set
			{
				if ((this._RoleId != value))
				{
					this.OnRoleIdChanging(value);
					this.SendPropertyChanging();
					this._RoleId = value;
					this.SendPropertyChanged("RoleId");
					this.OnRoleIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(200)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDeleted", DbType="Bit NOT NULL")]
		public bool IsDeleted
		{
			get
			{
				return this._IsDeleted;
			}
			set
			{
				if ((this._IsDeleted != value))
				{
					this.OnIsDeletedChanging(value);
					this.SendPropertyChanging();
					this._IsDeleted = value;
					this.SendPropertyChanged("IsDeleted");
					this.OnIsDeletedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Role_UserRole_Map", Storage="_UserRole_Maps", ThisKey="RoleId", OtherKey="RoleId")]
		public EntitySet<UserRole_Map> UserRole_Maps
		{
			get
			{
				return this._UserRole_Maps;
			}
			set
			{
				this._UserRole_Maps.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_UserRole_Maps(UserRole_Map entity)
		{
			this.SendPropertyChanging();
			entity.Role = this;
		}
		
		private void detach_UserRole_Maps(UserRole_Map entity)
		{
			this.SendPropertyChanging();
			entity.Role = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.UserRole_Map")]
	public partial class UserRole_Map : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserRoleMapId;
		
		private int _RoleId;
		
		private int _UserId;
		
		private bool _IsDeleted;
		
		private EntityRef<Role> _Role;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserRoleMapIdChanging(int value);
    partial void OnUserRoleMapIdChanged();
    partial void OnRoleIdChanging(int value);
    partial void OnRoleIdChanged();
    partial void OnUserIdChanging(int value);
    partial void OnUserIdChanged();
    partial void OnIsDeletedChanging(bool value);
    partial void OnIsDeletedChanged();
    #endregion
		
		public UserRole_Map()
		{
			this._Role = default(EntityRef<Role>);
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserRoleMapId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int UserRoleMapId
		{
			get
			{
				return this._UserRoleMapId;
			}
			set
			{
				if ((this._UserRoleMapId != value))
				{
					this.OnUserRoleMapIdChanging(value);
					this.SendPropertyChanging();
					this._UserRoleMapId = value;
					this.SendPropertyChanged("UserRoleMapId");
					this.OnUserRoleMapIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoleId", DbType="Int NOT NULL")]
		public int RoleId
		{
			get
			{
				return this._RoleId;
			}
			set
			{
				if ((this._RoleId != value))
				{
					if (this._Role.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRoleIdChanging(value);
					this.SendPropertyChanging();
					this._RoleId = value;
					this.SendPropertyChanged("RoleId");
					this.OnRoleIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserId", DbType="Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsDeleted", DbType="Bit NOT NULL")]
		public bool IsDeleted
		{
			get
			{
				return this._IsDeleted;
			}
			set
			{
				if ((this._IsDeleted != value))
				{
					this.OnIsDeletedChanging(value);
					this.SendPropertyChanging();
					this._IsDeleted = value;
					this.SendPropertyChanged("IsDeleted");
					this.OnIsDeletedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Role_UserRole_Map", Storage="_Role", ThisKey="RoleId", OtherKey="RoleId", IsForeignKey=true)]
		public Role Role
		{
			get
			{
				return this._Role.Entity;
			}
			set
			{
				Role previousValue = this._Role.Entity;
				if (((previousValue != value) 
							|| (this._Role.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Role.Entity = null;
						previousValue.UserRole_Maps.Remove(this);
					}
					this._Role.Entity = value;
					if ((value != null))
					{
						value.UserRole_Maps.Add(this);
						this._RoleId = value.RoleId;
					}
					else
					{
						this._RoleId = default(int);
					}
					this.SendPropertyChanged("Role");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserRole_Map", Storage="_User", ThisKey="UserId", OtherKey="UserId", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.UserRole_Maps.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.UserRole_Maps.Add(this);
						this._UserId = value.UserId;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591