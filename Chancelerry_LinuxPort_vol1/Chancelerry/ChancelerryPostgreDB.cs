// 
//  ____  _     __  __      _        _ 
// |  _ \| |__ |  \/  | ___| |_ __ _| |
// | | | | '_ \| |\/| |/ _ \ __/ _` | |
// | |_| | |_) | |  | |  __/ || (_| | |
// |____/|_.__/|_|  |_|\___|\__\__,_|_|
//
// Auto-generated from ChancelerryDB on 2016-03-02 14:36:05Z.
// Please visit http://code.google.com/p/dblinq2007/ for more information.
//
using System;
using System.ComponentModel;
using System.Data;
#if MONO_STRICT
	using System.Data.Linq;
#else   // MONO_STRICT
	using DbLinq.Data.Linq;
	using DbLinq.Vendor;
#endif  // MONO_STRICT
	using System.Data.Linq.Mapping;
using System.Diagnostics;


public partial class ChancelerryDb : DataContext
{
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		#endregion
	
	
	public ChancelerryDb(string connectionString) : 
			base(connectionString)
	{
		this.OnCreated();
	}
	
	public ChancelerryDb(string connection, MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		this.OnCreated();
	}
	
	public ChancelerryDb(IDbConnection connection, MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		this.OnCreated();
	}
	
	public Table<CollectedCards> CollectedCards
	{
		get
		{
			return this.GetTable<CollectedCards>();
		}
	}
	
	public Table<CollectedFieldsValues> CollectedFieldsValues
	{
		get
		{
			return this.GetTable<CollectedFieldsValues>();
		}
	}
	
	public Table<Dictionarys> Dictionarys
	{
		get
		{
			return this.GetTable<Dictionarys>();
		}
	}
	
	public Table<DictionarysValues> DictionarysValues
	{
		get
		{
			return this.GetTable<DictionarysValues>();
		}
	}
	
	public Table<Fields> Fields
	{
		get
		{
			return this.GetTable<Fields>();
		}
	}
	
	public Table<FieldsGroups> FieldsGroups
	{
		get
		{
			return this.GetTable<FieldsGroups>();
		}
	}
	
	public Table<Logs> Logs
	{
		get
		{
			return this.GetTable<Logs>();
		}
	}
	
	public Table<Registers> Registers
	{
		get
		{
			return this.GetTable<Registers>();
		}
	}
	
	public Table<RegistersModels> RegistersModels
	{
		get
		{
			return this.GetTable<RegistersModels>();
		}
	}
	
	public Table<RegistersUsersMap> RegistersUsersMap
	{
		get
		{
			return this.GetTable<RegistersUsersMap>();
		}
	}
	
	public Table<RegistersView> RegistersView
	{
		get
		{
			return this.GetTable<RegistersView>();
		}
	}
	
	public Table<Struct> Struct
	{
		get
		{
			return this.GetTable<Struct>();
		}
	}
	
	public Table<Users> Users
	{
		get
		{
			return this.GetTable<Users>();
		}
	}
}

#region Start MONO_STRICT
#if MONO_STRICT

public partial class ChancelerryDb
{
	
	public ChancelerryDb(IDbConnection connection) : 
			base(connection)
	{
		this.OnCreated();
	}
}
#region End MONO_STRICT
	#endregion
#else     // MONO_STRICT

public partial class ChancelerryDb
{
	
	public ChancelerryDb(IDbConnection connection) : 
			base(connection, new DbLinq.PostgreSql.PgsqlVendor())
	{
		this.OnCreated();
	}
	
	public ChancelerryDb(IDbConnection connection, IVendor sqlDialect) : 
			base(connection, sqlDialect)
	{
		this.OnCreated();
	}
	
	public ChancelerryDb(IDbConnection connection, MappingSource mappingSource, IVendor sqlDialect) : 
			base(connection, mappingSource, sqlDialect)
	{
		this.OnCreated();
	}
}
#region End Not MONO_STRICT
	#endregion
#endif     // MONO_STRICT
#endregion

[Table(Name="public.CollectedCards")]
public partial class CollectedCards : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private int _collectedCardID;
	
	private int _fkRegister;
	
	private EntityRef<Registers> _registers = new EntityRef<Registers>();
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnCollectedCardIDChanged();
		
		partial void OnCollectedCardIDChanging(int value);
		
		partial void OnFkRegisterChanged();
		
		partial void OnFkRegisterChanging(int value);
		#endregion
	
	
	public CollectedCards()
	{
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_collectedCardID", Name="collectedcardid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'collectedcards_collectedcardid_seq\')")]
	[DebuggerNonUserCode()]
	public int CollectedCardID
	{
		get
		{
			return this._collectedCardID;
		}
		set
		{
			if ((_collectedCardID != value))
			{
				this.OnCollectedCardIDChanging(value);
				this.SendPropertyChanging();
				this._collectedCardID = value;
				this.SendPropertyChanged("CollectedCardID");
				this.OnCollectedCardIDChanged();
			}
		}
	}
	
	[Column(Storage="_fkRegister", Name="fk_register", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkRegister
	{
		get
		{
			return this._fkRegister;
		}
		set
		{
			if ((_fkRegister != value))
			{
				this.OnFkRegisterChanging(value);
				this.SendPropertyChanging();
				this._fkRegister = value;
				this.SendPropertyChanged("FkRegister");
				this.OnFkRegisterChanged();
			}
		}
	}
	
	#region Parents
	[Association(Storage="_registers", OtherKey="RegisterID", ThisKey="FkRegister", Name="fk_collectedcards_collectedcards", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public Registers Registers
	{
		get
		{
			return this._registers.Entity;
		}
		set
		{
			if (((this._registers.Entity == value) 
						== false))
			{
				if ((this._registers.Entity != null))
				{
					Registers previousRegisters = this._registers.Entity;
					this._registers.Entity = null;
					previousRegisters.CollectedCards.Remove(this);
				}
				this._registers.Entity = value;
				if ((value != null))
				{
					value.CollectedCards.Add(this);
					_fkRegister = value.RegisterID;
				}
				else
				{
					_fkRegister = default(int);
				}
			}
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
}

[Table(Name="public.CollectedFieldsValues")]
public partial class CollectedFieldsValues : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private int _collectedFieldValueID;
	
	private System.DateTime _createDateTime;
	
	private int _fkCollectedCard;
	
	private int _fkField;
	
	private int _fkUser;
	
	private int _instance;
	
	private bool _isDeleted;
	
	private string _valueData;
	
	private System.Nullable<double> _valueFloat;
	
	private System.Nullable<int> _valueInt;
	
	private string _valueText;
	
	private int _version;
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnCollectedFieldValueIDChanged();
		
		partial void OnCollectedFieldValueIDChanging(int value);
		
		partial void OnCreateDateTimeChanged();
		
		partial void OnCreateDateTimeChanging(System.DateTime value);
		
		partial void OnFkCollectedCardChanged();
		
		partial void OnFkCollectedCardChanging(int value);
		
		partial void OnFkFieldChanged();
		
		partial void OnFkFieldChanging(int value);
		
		partial void OnFkUserChanged();
		
		partial void OnFkUserChanging(int value);
		
		partial void OnInstanceChanged();
		
		partial void OnInstanceChanging(int value);
		
		partial void OnIsDeletedChanged();
		
		partial void OnIsDeletedChanging(bool value);
		
		partial void OnValueDataChanged();
		
		partial void OnValueDataChanging(string value);
		
		partial void OnValueFloatChanged();
		
		partial void OnValueFloatChanging(System.Nullable<double> value);
		
		partial void OnValueIntChanged();
		
		partial void OnValueIntChanging(System.Nullable<int> value);
		
		partial void OnValueTextChanged();
		
		partial void OnValueTextChanging(string value);
		
		partial void OnVersionChanged();
		
		partial void OnVersionChanging(int value);
		#endregion
	
	
	public CollectedFieldsValues()
	{
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_collectedFieldValueID", Name="collectedfieldvalueid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'collectedfieldsvalues_collectedfieldvalueid_seq\')")]
	[DebuggerNonUserCode()]
	public int CollectedFieldValueID
	{
		get
		{
			return this._collectedFieldValueID;
		}
		set
		{
			if ((_collectedFieldValueID != value))
			{
				this.OnCollectedFieldValueIDChanging(value);
				this.SendPropertyChanging();
				this._collectedFieldValueID = value;
				this.SendPropertyChanged("CollectedFieldValueID");
				this.OnCollectedFieldValueIDChanged();
			}
		}
	}
	
	[Column(Storage="_createDateTime", Name="createdatetime", DbType="timestamp without time zone", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public System.DateTime CreateDateTime
	{
		get
		{
			return this._createDateTime;
		}
		set
		{
			if ((_createDateTime != value))
			{
				this.OnCreateDateTimeChanging(value);
				this.SendPropertyChanging();
				this._createDateTime = value;
				this.SendPropertyChanged("CreateDateTime");
				this.OnCreateDateTimeChanged();
			}
		}
	}
	
	[Column(Storage="_fkCollectedCard", Name="fk_collectedcard", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkCollectedCard
	{
		get
		{
			return this._fkCollectedCard;
		}
		set
		{
			if ((_fkCollectedCard != value))
			{
				this.OnFkCollectedCardChanging(value);
				this.SendPropertyChanging();
				this._fkCollectedCard = value;
				this.SendPropertyChanged("FkCollectedCard");
				this.OnFkCollectedCardChanged();
			}
		}
	}
	
	[Column(Storage="_fkField", Name="fk_field", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkField
	{
		get
		{
			return this._fkField;
		}
		set
		{
			if ((_fkField != value))
			{
				this.OnFkFieldChanging(value);
				this.SendPropertyChanging();
				this._fkField = value;
				this.SendPropertyChanged("FkField");
				this.OnFkFieldChanged();
			}
		}
	}
	
	[Column(Storage="_fkUser", Name="fk_user", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkUser
	{
		get
		{
			return this._fkUser;
		}
		set
		{
			if ((_fkUser != value))
			{
				this.OnFkUserChanging(value);
				this.SendPropertyChanging();
				this._fkUser = value;
				this.SendPropertyChanged("FkUser");
				this.OnFkUserChanged();
			}
		}
	}
	
	[Column(Storage="_instance", Name="instance", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int Instance
	{
		get
		{
			return this._instance;
		}
		set
		{
			if ((_instance != value))
			{
				this.OnInstanceChanging(value);
				this.SendPropertyChanging();
				this._instance = value;
				this.SendPropertyChanged("Instance");
				this.OnInstanceChanged();
			}
		}
	}
	
	[Column(Storage="_isDeleted", Name="isdeleted", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool IsDeleted
	{
		get
		{
			return this._isDeleted;
		}
		set
		{
			if ((_isDeleted != value))
			{
				this.OnIsDeletedChanging(value);
				this.SendPropertyChanging();
				this._isDeleted = value;
				this.SendPropertyChanged("IsDeleted");
				this.OnIsDeletedChanged();
			}
		}
	}
	
	[Column(Storage="_valueData", Name="valuedata", DbType="text", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string ValueData
	{
		get
		{
			return this._valueData;
		}
		set
		{
			if (((_valueData == value) 
						== false))
			{
				this.OnValueDataChanging(value);
				this.SendPropertyChanging();
				this._valueData = value;
				this.SendPropertyChanged("ValueData");
				this.OnValueDataChanged();
			}
		}
	}
	
	[Column(Storage="_valueFloat", Name="valuefloat", DbType="double precision", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<double> ValueFloat
	{
		get
		{
			return this._valueFloat;
		}
		set
		{
			if ((_valueFloat != value))
			{
				this.OnValueFloatChanging(value);
				this.SendPropertyChanging();
				this._valueFloat = value;
				this.SendPropertyChanged("ValueFloat");
				this.OnValueFloatChanged();
			}
		}
	}
	
	[Column(Storage="_valueInt", Name="valueint", DbType="integer(32,0)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<int> ValueInt
	{
		get
		{
			return this._valueInt;
		}
		set
		{
			if ((_valueInt != value))
			{
				this.OnValueIntChanging(value);
				this.SendPropertyChanging();
				this._valueInt = value;
				this.SendPropertyChanged("ValueInt");
				this.OnValueIntChanged();
			}
		}
	}
	
	[Column(Storage="_valueText", Name="valuetext", DbType="text", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string ValueText
	{
		get
		{
			return this._valueText;
		}
		set
		{
			if (((_valueText == value) 
						== false))
			{
				this.OnValueTextChanging(value);
				this.SendPropertyChanging();
				this._valueText = value;
				this.SendPropertyChanged("ValueText");
				this.OnValueTextChanged();
			}
		}
	}
	
	[Column(Storage="_version", Name="version", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int Version
	{
		get
		{
			return this._version;
		}
		set
		{
			if ((_version != value))
			{
				this.OnVersionChanging(value);
				this.SendPropertyChanging();
				this._version = value;
				this.SendPropertyChanged("Version");
				this.OnVersionChanged();
			}
		}
	}
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
}

[Table(Name="public.Dictionarys")]
public partial class Dictionarys : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private string _description;
	
	private int _dictionaryID;
	
	private string _name;
	
	private EntitySet<DictionarysValues> _dictionarySvAlues;
	
	private EntitySet<Fields> _fields;
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnDictionaryIDChanged();
		
		partial void OnDictionaryIDChanging(int value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		#endregion
	
	
	public Dictionarys()
	{
		_dictionarySvAlues = new EntitySet<DictionarysValues>(new Action<DictionarysValues>(this.DictionarySValues_Attach), new Action<DictionarysValues>(this.DictionarySValues_Detach));
		_fields = new EntitySet<Fields>(new Action<Fields>(this.Fields_Attach), new Action<Fields>(this.Fields_Detach));
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_description", Name="description", DbType="text", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Description
	{
		get
		{
			return this._description;
		}
		set
		{
			if (((_description == value) 
						== false))
			{
				this.OnDescriptionChanging(value);
				this.SendPropertyChanging();
				this._description = value;
				this.SendPropertyChanged("Description");
				this.OnDescriptionChanged();
			}
		}
	}
	
	[Column(Storage="_dictionaryID", Name="dictionaryid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'dictionarys_dictionaryid_seq\')")]
	[DebuggerNonUserCode()]
	public int DictionaryID
	{
		get
		{
			return this._dictionaryID;
		}
		set
		{
			if ((_dictionaryID != value))
			{
				this.OnDictionaryIDChanging(value);
				this.SendPropertyChanging();
				this._dictionaryID = value;
				this.SendPropertyChanged("DictionaryID");
				this.OnDictionaryIDChanged();
			}
		}
	}
	
	[Column(Storage="_name", Name="name", DbType="character varying(200)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public string Name
	{
		get
		{
			return this._name;
		}
		set
		{
			if (((_name == value) 
						== false))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	#region Children
	[Association(Storage="_dictionarySvAlues", OtherKey="FkDictionary", ThisKey="DictionaryID", Name="fk_dictionarysvalues_dictionarys")]
	[DebuggerNonUserCode()]
	public EntitySet<DictionarysValues> DictionarySValues
	{
		get
		{
			return this._dictionarySvAlues;
		}
		set
		{
			this._dictionarySvAlues = value;
		}
	}
	
	[Association(Storage="_fields", OtherKey="FkDictionary", ThisKey="DictionaryID", Name="fk_fields_dictionarys")]
	[DebuggerNonUserCode()]
	public EntitySet<Fields> Fields
	{
		get
		{
			return this._fields;
		}
		set
		{
			this._fields = value;
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
	
	#region Attachment handlers
	private void DictionarySValues_Attach(DictionarysValues entity)
	{
		this.SendPropertyChanging();
		entity.DictionaryS = this;
	}
	
	private void DictionarySValues_Detach(DictionarysValues entity)
	{
		this.SendPropertyChanging();
		entity.DictionaryS = null;
	}
	
	private void Fields_Attach(Fields entity)
	{
		this.SendPropertyChanging();
		entity.DictionaryS = this;
	}
	
	private void Fields_Detach(Fields entity)
	{
		this.SendPropertyChanging();
		entity.DictionaryS = null;
	}
	#endregion
}

[Table(Name="public.DictionarysValues")]
public partial class DictionarysValues : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private int _dictionaryValueID;
	
	private int _fkDictionary;
	
	private string _value;
	
	private EntityRef<Dictionarys> _dictionaryS = new EntityRef<Dictionarys>();
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnDictionaryValueIDChanged();
		
		partial void OnDictionaryValueIDChanging(int value);
		
		partial void OnFkDictionaryChanged();
		
		partial void OnFkDictionaryChanging(int value);
		
		partial void OnValueChanged();
		
		partial void OnValueChanging(string value);
		#endregion
	
	
	public DictionarysValues()
	{
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_dictionaryValueID", Name="dictionaryvalueid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'dictionarysvalues_dictionaryvalueid_seq\')")]
	[DebuggerNonUserCode()]
	public int DictionaryValueID
	{
		get
		{
			return this._dictionaryValueID;
		}
		set
		{
			if ((_dictionaryValueID != value))
			{
				this.OnDictionaryValueIDChanging(value);
				this.SendPropertyChanging();
				this._dictionaryValueID = value;
				this.SendPropertyChanged("DictionaryValueID");
				this.OnDictionaryValueIDChanged();
			}
		}
	}
	
	[Column(Storage="_fkDictionary", Name="fk_dictionary", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkDictionary
	{
		get
		{
			return this._fkDictionary;
		}
		set
		{
			if ((_fkDictionary != value))
			{
				this.OnFkDictionaryChanging(value);
				this.SendPropertyChanging();
				this._fkDictionary = value;
				this.SendPropertyChanged("FkDictionary");
				this.OnFkDictionaryChanged();
			}
		}
	}
	
	[Column(Storage="_value", Name="value", DbType="text", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public string Value
	{
		get
		{
			return this._value;
		}
		set
		{
			if (((_value == value) 
						== false))
			{
				this.OnValueChanging(value);
				this.SendPropertyChanging();
				this._value = value;
				this.SendPropertyChanged("Value");
				this.OnValueChanged();
			}
		}
	}
	
	#region Parents
	[Association(Storage="_dictionaryS", OtherKey="DictionaryID", ThisKey="FkDictionary", Name="fk_dictionarysvalues_dictionarys", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public Dictionarys DictionaryS
	{
		get
		{
			return this._dictionaryS.Entity;
		}
		set
		{
			if (((this._dictionaryS.Entity == value) 
						== false))
			{
				if ((this._dictionaryS.Entity != null))
				{
					Dictionarys previousDictionarys = this._dictionaryS.Entity;
					this._dictionaryS.Entity = null;
					previousDictionarys.DictionarySValues.Remove(this);
				}
				this._dictionaryS.Entity = value;
				if ((value != null))
				{
					value.DictionarySValues.Add(this);
					_fkDictionary = value.DictionaryID;
				}
				else
				{
					_fkDictionary = default(int);
				}
			}
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
}

[Table(Name="public.Fields")]
public partial class Fields : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private int _columnNumber;
	
	private string _description;
	
	private int _fieldID;
	
	private System.Nullable<int> _fkDictionary;
	
	private int _fkFieldsGroup;
	
	private int _height;
	
	private int _line;
	
	private bool _mandatory;
	
	private System.Nullable<bool> _multiple;
	
	private string _name;
	
	private string _type;
	
	private int _width;
	
	private EntitySet<RegistersView> _registersView;
	
	private EntityRef<FieldsGroups> _fieldsGroups = new EntityRef<FieldsGroups>();
	
	private EntityRef<Dictionarys> _dictionaryS = new EntityRef<Dictionarys>();
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnColumnNumberChanged();
		
		partial void OnColumnNumberChanging(int value);
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnFieldIDChanged();
		
		partial void OnFieldIDChanging(int value);
		
		partial void OnFkDictionaryChanged();
		
		partial void OnFkDictionaryChanging(System.Nullable<int> value);
		
		partial void OnFkFieldsGroupChanged();
		
		partial void OnFkFieldsGroupChanging(int value);
		
		partial void OnHeightChanged();
		
		partial void OnHeightChanging(int value);
		
		partial void OnLineChanged();
		
		partial void OnLineChanging(int value);
		
		partial void OnMandatoryChanged();
		
		partial void OnMandatoryChanging(bool value);
		
		partial void OnMultipleChanged();
		
		partial void OnMultipleChanging(System.Nullable<bool> value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnTypeChanged();
		
		partial void OnTypeChanging(string value);
		
		partial void OnWidthChanged();
		
		partial void OnWidthChanging(int value);
		#endregion
	
	
	public Fields()
	{
		_registersView = new EntitySet<RegistersView>(new Action<RegistersView>(this.RegistersView_Attach), new Action<RegistersView>(this.RegistersView_Detach));
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_columnNumber", Name="columnnumber", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int ColumnNumber
	{
		get
		{
			return this._columnNumber;
		}
		set
		{
			if ((_columnNumber != value))
			{
				this.OnColumnNumberChanging(value);
				this.SendPropertyChanging();
				this._columnNumber = value;
				this.SendPropertyChanged("ColumnNumber");
				this.OnColumnNumberChanged();
			}
		}
	}
	
	[Column(Storage="_description", Name="description", DbType="text", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Description
	{
		get
		{
			return this._description;
		}
		set
		{
			if (((_description == value) 
						== false))
			{
				this.OnDescriptionChanging(value);
				this.SendPropertyChanging();
				this._description = value;
				this.SendPropertyChanged("Description");
				this.OnDescriptionChanged();
			}
		}
	}
	
	[Column(Storage="_fieldID", Name="fieldid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'fields_fieldid_seq\')")]
	[DebuggerNonUserCode()]
	public int FieldID
	{
		get
		{
			return this._fieldID;
		}
		set
		{
			if ((_fieldID != value))
			{
				this.OnFieldIDChanging(value);
				this.SendPropertyChanging();
				this._fieldID = value;
				this.SendPropertyChanged("FieldID");
				this.OnFieldIDChanged();
			}
		}
	}
	
	[Column(Storage="_fkDictionary", Name="fk_dictionary", DbType="integer(32,0)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<int> FkDictionary
	{
		get
		{
			return this._fkDictionary;
		}
		set
		{
			if ((_fkDictionary != value))
			{
				this.OnFkDictionaryChanging(value);
				this.SendPropertyChanging();
				this._fkDictionary = value;
				this.SendPropertyChanged("FkDictionary");
				this.OnFkDictionaryChanged();
			}
		}
	}
	
	[Column(Storage="_fkFieldsGroup", Name="fk_fieldsgroup", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkFieldsGroup
	{
		get
		{
			return this._fkFieldsGroup;
		}
		set
		{
			if ((_fkFieldsGroup != value))
			{
				this.OnFkFieldsGroupChanging(value);
				this.SendPropertyChanging();
				this._fkFieldsGroup = value;
				this.SendPropertyChanged("FkFieldsGroup");
				this.OnFkFieldsGroupChanged();
			}
		}
	}
	
	[Column(Storage="_height", Name="height", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int Height
	{
		get
		{
			return this._height;
		}
		set
		{
			if ((_height != value))
			{
				this.OnHeightChanging(value);
				this.SendPropertyChanging();
				this._height = value;
				this.SendPropertyChanged("Height");
				this.OnHeightChanged();
			}
		}
	}
	
	[Column(Storage="_line", Name="line", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int Line
	{
		get
		{
			return this._line;
		}
		set
		{
			if ((_line != value))
			{
				this.OnLineChanging(value);
				this.SendPropertyChanging();
				this._line = value;
				this.SendPropertyChanged("Line");
				this.OnLineChanged();
			}
		}
	}
	
	[Column(Storage="_mandatory", Name="mandatory", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Mandatory
	{
		get
		{
			return this._mandatory;
		}
		set
		{
			if ((_mandatory != value))
			{
				this.OnMandatoryChanging(value);
				this.SendPropertyChanging();
				this._mandatory = value;
				this.SendPropertyChanged("Mandatory");
				this.OnMandatoryChanged();
			}
		}
	}
	
	[Column(Storage="_multiple", Name="multiple", DbType="boolean", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<bool> Multiple
	{
		get
		{
			return this._multiple;
		}
		set
		{
			if ((_multiple != value))
			{
				this.OnMultipleChanging(value);
				this.SendPropertyChanging();
				this._multiple = value;
				this.SendPropertyChanged("Multiple");
				this.OnMultipleChanged();
			}
		}
	}
	
	[Column(Storage="_name", Name="name", DbType="character varying(200)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public string Name
	{
		get
		{
			return this._name;
		}
		set
		{
			if (((_name == value) 
						== false))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	[Column(Storage="_type", Name="type", DbType="character varying(50)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public string Type
	{
		get
		{
			return this._type;
		}
		set
		{
			if (((_type == value) 
						== false))
			{
				this.OnTypeChanging(value);
				this.SendPropertyChanging();
				this._type = value;
				this.SendPropertyChanged("Type");
				this.OnTypeChanged();
			}
		}
	}
	
	[Column(Storage="_width", Name="width", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int Width
	{
		get
		{
			return this._width;
		}
		set
		{
			if ((_width != value))
			{
				this.OnWidthChanging(value);
				this.SendPropertyChanging();
				this._width = value;
				this.SendPropertyChanged("Width");
				this.OnWidthChanged();
			}
		}
	}
	
	#region Children
	[Association(Storage="_registersView", OtherKey="FkField", ThisKey="FieldID", Name="fk_registersview_fields")]
	[DebuggerNonUserCode()]
	public EntitySet<RegistersView> RegistersView
	{
		get
		{
			return this._registersView;
		}
		set
		{
			this._registersView = value;
		}
	}
	#endregion
	
	#region Parents
	[Association(Storage="_fieldsGroups", OtherKey="FieldsGroupID", ThisKey="FkFieldsGroup", Name="fk_fields_fields", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public FieldsGroups FieldsGroups
	{
		get
		{
			return this._fieldsGroups.Entity;
		}
		set
		{
			if (((this._fieldsGroups.Entity == value) 
						== false))
			{
				if ((this._fieldsGroups.Entity != null))
				{
					FieldsGroups previousFieldsGroups = this._fieldsGroups.Entity;
					this._fieldsGroups.Entity = null;
					previousFieldsGroups.Fields.Remove(this);
				}
				this._fieldsGroups.Entity = value;
				if ((value != null))
				{
					value.Fields.Add(this);
					_fkFieldsGroup = value.FieldsGroupID;
				}
				else
				{
					_fkFieldsGroup = default(int);
				}
			}
		}
	}
	
	[Association(Storage="_dictionaryS", OtherKey="DictionaryID", ThisKey="FkDictionary", Name="fk_fields_dictionarys", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public Dictionarys DictionaryS
	{
		get
		{
			return this._dictionaryS.Entity;
		}
		set
		{
			if (((this._dictionaryS.Entity == value) 
						== false))
			{
				if ((this._dictionaryS.Entity != null))
				{
					Dictionarys previousDictionarys = this._dictionaryS.Entity;
					this._dictionaryS.Entity = null;
					previousDictionarys.Fields.Remove(this);
				}
				this._dictionaryS.Entity = value;
				if ((value != null))
				{
					value.Fields.Add(this);
					_fkDictionary = value.DictionaryID;
				}
				else
				{
					_fkDictionary = null;
				}
			}
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
	
	#region Attachment handlers
	private void RegistersView_Attach(RegistersView entity)
	{
		this.SendPropertyChanging();
		entity.Fields = this;
	}
	
	private void RegistersView_Detach(RegistersView entity)
	{
		this.SendPropertyChanging();
		entity.Fields = null;
	}
	#endregion
}

[Table(Name="public.FieldsGroups")]
public partial class FieldsGroups : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private int _fieldsGroupID;
	
	private int _fkRegisterModel;
	
	private int _line;
	
	private bool _multiple;
	
	private string _name;
	
	private EntitySet<Fields> _fields;
	
	private EntityRef<RegistersModels> _registersModels = new EntityRef<RegistersModels>();
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnFieldsGroupIDChanged();
		
		partial void OnFieldsGroupIDChanging(int value);
		
		partial void OnFkRegisterModelChanged();
		
		partial void OnFkRegisterModelChanging(int value);
		
		partial void OnLineChanged();
		
		partial void OnLineChanging(int value);
		
		partial void OnMultipleChanged();
		
		partial void OnMultipleChanging(bool value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		#endregion
	
	
	public FieldsGroups()
	{
		_fields = new EntitySet<Fields>(new Action<Fields>(this.Fields_Attach), new Action<Fields>(this.Fields_Detach));
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_fieldsGroupID", Name="fieldsgroupid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'fieldsgroups_fieldsgroupid_seq\')")]
	[DebuggerNonUserCode()]
	public int FieldsGroupID
	{
		get
		{
			return this._fieldsGroupID;
		}
		set
		{
			if ((_fieldsGroupID != value))
			{
				this.OnFieldsGroupIDChanging(value);
				this.SendPropertyChanging();
				this._fieldsGroupID = value;
				this.SendPropertyChanged("FieldsGroupID");
				this.OnFieldsGroupIDChanged();
			}
		}
	}
	
	[Column(Storage="_fkRegisterModel", Name="fk_registermodel", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkRegisterModel
	{
		get
		{
			return this._fkRegisterModel;
		}
		set
		{
			if ((_fkRegisterModel != value))
			{
				this.OnFkRegisterModelChanging(value);
				this.SendPropertyChanging();
				this._fkRegisterModel = value;
				this.SendPropertyChanged("FkRegisterModel");
				this.OnFkRegisterModelChanged();
			}
		}
	}
	
	[Column(Storage="_line", Name="line", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int Line
	{
		get
		{
			return this._line;
		}
		set
		{
			if ((_line != value))
			{
				this.OnLineChanging(value);
				this.SendPropertyChanging();
				this._line = value;
				this.SendPropertyChanged("Line");
				this.OnLineChanged();
			}
		}
	}
	
	[Column(Storage="_multiple", Name="multiple", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Multiple
	{
		get
		{
			return this._multiple;
		}
		set
		{
			if ((_multiple != value))
			{
				this.OnMultipleChanging(value);
				this.SendPropertyChanging();
				this._multiple = value;
				this.SendPropertyChanged("Multiple");
				this.OnMultipleChanged();
			}
		}
	}
	
	[Column(Storage="_name", Name="name", DbType="character varying(200)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public string Name
	{
		get
		{
			return this._name;
		}
		set
		{
			if (((_name == value) 
						== false))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	#region Children
	[Association(Storage="_fields", OtherKey="FkFieldsGroup", ThisKey="FieldsGroupID", Name="fk_fields_fields")]
	[DebuggerNonUserCode()]
	public EntitySet<Fields> Fields
	{
		get
		{
			return this._fields;
		}
		set
		{
			this._fields = value;
		}
	}
	#endregion
	
	#region Parents
	[Association(Storage="_registersModels", OtherKey="RegisterModelID", ThisKey="FkRegisterModel", Name="fk_fieldsgroups_registersmodels", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public RegistersModels RegistersModels
	{
		get
		{
			return this._registersModels.Entity;
		}
		set
		{
			if (((this._registersModels.Entity == value) 
						== false))
			{
				if ((this._registersModels.Entity != null))
				{
					RegistersModels previousRegistersModels = this._registersModels.Entity;
					this._registersModels.Entity = null;
					previousRegistersModels.FieldsGroups.Remove(this);
				}
				this._registersModels.Entity = value;
				if ((value != null))
				{
					value.FieldsGroups.Add(this);
					_fkRegisterModel = value.RegisterModelID;
				}
				else
				{
					_fkRegisterModel = default(int);
				}
			}
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
	
	#region Attachment handlers
	private void Fields_Attach(Fields entity)
	{
		this.SendPropertyChanging();
		entity.FieldsGroups = this;
	}
	
	private void Fields_Detach(Fields entity)
	{
		this.SendPropertyChanging();
		entity.FieldsGroups = null;
	}
	#endregion
}

[Table(Name="public.Logs")]
public partial class Logs : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private System.Nullable<bool> _active;
	
	private System.Nullable<System.DateTime> _createDateTime;
	
	private string _description;
	
	private System.Nullable<int> _fkUsers;
	
	private int _logID;
	
	private System.Nullable<int> _type;
	
	private EntityRef<Users> _users = new EntityRef<Users>();
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(System.Nullable<bool> value);
		
		partial void OnCreateDateTimeChanged();
		
		partial void OnCreateDateTimeChanging(System.Nullable<System.DateTime> value);
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnFkUsersChanged();
		
		partial void OnFkUsersChanging(System.Nullable<int> value);
		
		partial void OnLogIDChanged();
		
		partial void OnLogIDChanging(int value);
		
		partial void OnTypeChanged();
		
		partial void OnTypeChanging(System.Nullable<int> value);
		#endregion
	
	
	public Logs()
	{
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<bool> Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_createDateTime", Name="createdatetime", DbType="timestamp without time zone", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<System.DateTime> CreateDateTime
	{
		get
		{
			return this._createDateTime;
		}
		set
		{
			if ((_createDateTime != value))
			{
				this.OnCreateDateTimeChanging(value);
				this.SendPropertyChanging();
				this._createDateTime = value;
				this.SendPropertyChanged("CreateDateTime");
				this.OnCreateDateTimeChanged();
			}
		}
	}
	
	[Column(Storage="_description", Name="description", DbType="text", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Description
	{
		get
		{
			return this._description;
		}
		set
		{
			if (((_description == value) 
						== false))
			{
				this.OnDescriptionChanging(value);
				this.SendPropertyChanging();
				this._description = value;
				this.SendPropertyChanged("Description");
				this.OnDescriptionChanged();
			}
		}
	}
	
	[Column(Storage="_fkUsers", Name="fk_users", DbType="integer(32,0)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<int> FkUsers
	{
		get
		{
			return this._fkUsers;
		}
		set
		{
			if ((_fkUsers != value))
			{
				this.OnFkUsersChanging(value);
				this.SendPropertyChanging();
				this._fkUsers = value;
				this.SendPropertyChanged("FkUsers");
				this.OnFkUsersChanged();
			}
		}
	}
	
	[Column(Storage="_logID", Name="logid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'logs_logid_seq\')")]
	[DebuggerNonUserCode()]
	public int LogID
	{
		get
		{
			return this._logID;
		}
		set
		{
			if ((_logID != value))
			{
				this.OnLogIDChanging(value);
				this.SendPropertyChanging();
				this._logID = value;
				this.SendPropertyChanged("LogID");
				this.OnLogIDChanged();
			}
		}
	}
	
	[Column(Storage="_type", Name="type", DbType="integer(32,0)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<int> Type
	{
		get
		{
			return this._type;
		}
		set
		{
			if ((_type != value))
			{
				this.OnTypeChanging(value);
				this.SendPropertyChanging();
				this._type = value;
				this.SendPropertyChanged("Type");
				this.OnTypeChanged();
			}
		}
	}
	
	#region Parents
	[Association(Storage="_users", OtherKey="UserID", ThisKey="FkUsers", Name="fk_logs_users", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public Users Users
	{
		get
		{
			return this._users.Entity;
		}
		set
		{
			if (((this._users.Entity == value) 
						== false))
			{
				if ((this._users.Entity != null))
				{
					Users previousUsers = this._users.Entity;
					this._users.Entity = null;
					previousUsers.Logs.Remove(this);
				}
				this._users.Entity = value;
				if ((value != null))
				{
					value.Logs.Add(this);
					_fkUsers = value.UserID;
				}
				else
				{
					_fkUsers = null;
				}
			}
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
}

[Table(Name="public.Registers")]
public partial class Registers : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private int _fkRegistersModel;
	
	private string _name;
	
	private int _registerID;
	
	private EntitySet<CollectedCards> _collectedCards;
	
	private EntitySet<RegistersUsersMap> _registersUsersMap;
	
	private EntityRef<RegistersModels> _registersModels = new EntityRef<RegistersModels>();
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnFkRegistersModelChanged();
		
		partial void OnFkRegistersModelChanging(int value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnRegisterIDChanged();
		
		partial void OnRegisterIDChanging(int value);
		#endregion
	
	
	public Registers()
	{
		_collectedCards = new EntitySet<CollectedCards>(new Action<CollectedCards>(this.CollectedCards_Attach), new Action<CollectedCards>(this.CollectedCards_Detach));
		_registersUsersMap = new EntitySet<RegistersUsersMap>(new Action<RegistersUsersMap>(this.RegistersUsersMap_Attach), new Action<RegistersUsersMap>(this.RegistersUsersMap_Detach));
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_fkRegistersModel", Name="fk_registersmodel", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkRegistersModel
	{
		get
		{
			return this._fkRegistersModel;
		}
		set
		{
			if ((_fkRegistersModel != value))
			{
				this.OnFkRegistersModelChanging(value);
				this.SendPropertyChanging();
				this._fkRegistersModel = value;
				this.SendPropertyChanged("FkRegistersModel");
				this.OnFkRegistersModelChanged();
			}
		}
	}
	
	[Column(Storage="_name", Name="name", DbType="text", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Name
	{
		get
		{
			return this._name;
		}
		set
		{
			if (((_name == value) 
						== false))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	[Column(Storage="_registerID", Name="registerid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'registers_registerid_seq\')")]
	[DebuggerNonUserCode()]
	public int RegisterID
	{
		get
		{
			return this._registerID;
		}
		set
		{
			if ((_registerID != value))
			{
				this.OnRegisterIDChanging(value);
				this.SendPropertyChanging();
				this._registerID = value;
				this.SendPropertyChanged("RegisterID");
				this.OnRegisterIDChanged();
			}
		}
	}
	
	#region Children
	[Association(Storage="_collectedCards", OtherKey="FkRegister", ThisKey="RegisterID", Name="fk_collectedcards_collectedcards")]
	[DebuggerNonUserCode()]
	public EntitySet<CollectedCards> CollectedCards
	{
		get
		{
			return this._collectedCards;
		}
		set
		{
			this._collectedCards = value;
		}
	}
	
	[Association(Storage="_registersUsersMap", OtherKey="FkRegister", ThisKey="RegisterID", Name="fk_registersusersmap_registers")]
	[DebuggerNonUserCode()]
	public EntitySet<RegistersUsersMap> RegistersUsersMap
	{
		get
		{
			return this._registersUsersMap;
		}
		set
		{
			this._registersUsersMap = value;
		}
	}
	#endregion
	
	#region Parents
	[Association(Storage="_registersModels", OtherKey="RegisterModelID", ThisKey="FkRegistersModel", Name="fk_registers_registersmodels", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public RegistersModels RegistersModels
	{
		get
		{
			return this._registersModels.Entity;
		}
		set
		{
			if (((this._registersModels.Entity == value) 
						== false))
			{
				if ((this._registersModels.Entity != null))
				{
					RegistersModels previousRegistersModels = this._registersModels.Entity;
					this._registersModels.Entity = null;
					previousRegistersModels.Registers.Remove(this);
				}
				this._registersModels.Entity = value;
				if ((value != null))
				{
					value.Registers.Add(this);
					_fkRegistersModel = value.RegisterModelID;
				}
				else
				{
					_fkRegistersModel = default(int);
				}
			}
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
	
	#region Attachment handlers
	private void CollectedCards_Attach(CollectedCards entity)
	{
		this.SendPropertyChanging();
		entity.Registers = this;
	}
	
	private void CollectedCards_Detach(CollectedCards entity)
	{
		this.SendPropertyChanging();
		entity.Registers = null;
	}
	
	private void RegistersUsersMap_Attach(RegistersUsersMap entity)
	{
		this.SendPropertyChanging();
		entity.Registers = this;
	}
	
	private void RegistersUsersMap_Detach(RegistersUsersMap entity)
	{
		this.SendPropertyChanging();
		entity.Registers = null;
	}
	#endregion
}

[Table(Name="public.RegistersModels")]
public partial class RegistersModels : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private string _name;
	
	private int _registerModelID;
	
	private EntitySet<FieldsGroups> _fieldsGroups;
	
	private EntitySet<Registers> _registers;
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnRegisterModelIDChanged();
		
		partial void OnRegisterModelIDChanging(int value);
		#endregion
	
	
	public RegistersModels()
	{
		_fieldsGroups = new EntitySet<FieldsGroups>(new Action<FieldsGroups>(this.FieldsGroups_Attach), new Action<FieldsGroups>(this.FieldsGroups_Detach));
		_registers = new EntitySet<Registers>(new Action<Registers>(this.Registers_Attach), new Action<Registers>(this.Registers_Detach));
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_name", Name="name", DbType="character varying(200)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public string Name
	{
		get
		{
			return this._name;
		}
		set
		{
			if (((_name == value) 
						== false))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	[Column(Storage="_registerModelID", Name="registermodelid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'registersmodels_registermodelid_seq\')")]
	[DebuggerNonUserCode()]
	public int RegisterModelID
	{
		get
		{
			return this._registerModelID;
		}
		set
		{
			if ((_registerModelID != value))
			{
				this.OnRegisterModelIDChanging(value);
				this.SendPropertyChanging();
				this._registerModelID = value;
				this.SendPropertyChanged("RegisterModelID");
				this.OnRegisterModelIDChanged();
			}
		}
	}
	
	#region Children
	[Association(Storage="_fieldsGroups", OtherKey="FkRegisterModel", ThisKey="RegisterModelID", Name="fk_fieldsgroups_registersmodels")]
	[DebuggerNonUserCode()]
	public EntitySet<FieldsGroups> FieldsGroups
	{
		get
		{
			return this._fieldsGroups;
		}
		set
		{
			this._fieldsGroups = value;
		}
	}
	
	[Association(Storage="_registers", OtherKey="FkRegistersModel", ThisKey="RegisterModelID", Name="fk_registers_registersmodels")]
	[DebuggerNonUserCode()]
	public EntitySet<Registers> Registers
	{
		get
		{
			return this._registers;
		}
		set
		{
			this._registers = value;
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
	
	#region Attachment handlers
	private void FieldsGroups_Attach(FieldsGroups entity)
	{
		this.SendPropertyChanging();
		entity.RegistersModels = this;
	}
	
	private void FieldsGroups_Detach(FieldsGroups entity)
	{
		this.SendPropertyChanging();
		entity.RegistersModels = null;
	}
	
	private void Registers_Attach(Registers entity)
	{
		this.SendPropertyChanging();
		entity.RegistersModels = this;
	}
	
	private void Registers_Detach(Registers entity)
	{
		this.SendPropertyChanging();
		entity.RegistersModels = null;
	}
	#endregion
}

[Table(Name="public.RegistersUsersMap")]
public partial class RegistersUsersMap : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private bool _canEdit;
	
	private int _fkRegister;
	
	private int _fkUser;
	
	private int _registersUsersMapID;
	
	private EntitySet<RegistersView> _registersView;
	
	private EntityRef<Users> _users = new EntityRef<Users>();
	
	private EntityRef<Registers> _registers = new EntityRef<Registers>();
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnCanEditChanged();
		
		partial void OnCanEditChanging(bool value);
		
		partial void OnFkRegisterChanged();
		
		partial void OnFkRegisterChanging(int value);
		
		partial void OnFkUserChanged();
		
		partial void OnFkUserChanging(int value);
		
		partial void OnRegistersUsersMapIDChanged();
		
		partial void OnRegistersUsersMapIDChanging(int value);
		#endregion
	
	
	public RegistersUsersMap()
	{
		_registersView = new EntitySet<RegistersView>(new Action<RegistersView>(this.RegistersView_Attach), new Action<RegistersView>(this.RegistersView_Detach));
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_canEdit", Name="canedit", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool CanEdit
	{
		get
		{
			return this._canEdit;
		}
		set
		{
			if ((_canEdit != value))
			{
				this.OnCanEditChanging(value);
				this.SendPropertyChanging();
				this._canEdit = value;
				this.SendPropertyChanged("CanEdit");
				this.OnCanEditChanged();
			}
		}
	}
	
	[Column(Storage="_fkRegister", Name="fk_register", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkRegister
	{
		get
		{
			return this._fkRegister;
		}
		set
		{
			if ((_fkRegister != value))
			{
				this.OnFkRegisterChanging(value);
				this.SendPropertyChanging();
				this._fkRegister = value;
				this.SendPropertyChanged("FkRegister");
				this.OnFkRegisterChanged();
			}
		}
	}
	
	[Column(Storage="_fkUser", Name="fk_user", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkUser
	{
		get
		{
			return this._fkUser;
		}
		set
		{
			if ((_fkUser != value))
			{
				this.OnFkUserChanging(value);
				this.SendPropertyChanging();
				this._fkUser = value;
				this.SendPropertyChanged("FkUser");
				this.OnFkUserChanged();
			}
		}
	}
	
	[Column(Storage="_registersUsersMapID", Name="registersusersmapid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'registersusersmap_registersusersmapid_seq\')")]
	[DebuggerNonUserCode()]
	public int RegistersUsersMapID
	{
		get
		{
			return this._registersUsersMapID;
		}
		set
		{
			if ((_registersUsersMapID != value))
			{
				this.OnRegistersUsersMapIDChanging(value);
				this.SendPropertyChanging();
				this._registersUsersMapID = value;
				this.SendPropertyChanged("RegistersUsersMapID");
				this.OnRegistersUsersMapIDChanged();
			}
		}
	}
	
	#region Children
	[Association(Storage="_registersView", OtherKey="FkRegistersUsersMap", ThisKey="RegistersUsersMapID", Name="fk_registersview_registersusersmap")]
	[DebuggerNonUserCode()]
	public EntitySet<RegistersView> RegistersView
	{
		get
		{
			return this._registersView;
		}
		set
		{
			this._registersView = value;
		}
	}
	#endregion
	
	#region Parents
	[Association(Storage="_users", OtherKey="UserID", ThisKey="FkUser", Name="fk_registersusersmap_users", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public Users Users
	{
		get
		{
			return this._users.Entity;
		}
		set
		{
			if (((this._users.Entity == value) 
						== false))
			{
				if ((this._users.Entity != null))
				{
					Users previousUsers = this._users.Entity;
					this._users.Entity = null;
					previousUsers.RegistersUsersMap.Remove(this);
				}
				this._users.Entity = value;
				if ((value != null))
				{
					value.RegistersUsersMap.Add(this);
					_fkUser = value.UserID;
				}
				else
				{
					_fkUser = default(int);
				}
			}
		}
	}
	
	[Association(Storage="_registers", OtherKey="RegisterID", ThisKey="FkRegister", Name="fk_registersusersmap_registers", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public Registers Registers
	{
		get
		{
			return this._registers.Entity;
		}
		set
		{
			if (((this._registers.Entity == value) 
						== false))
			{
				if ((this._registers.Entity != null))
				{
					Registers previousRegisters = this._registers.Entity;
					this._registers.Entity = null;
					previousRegisters.RegistersUsersMap.Remove(this);
				}
				this._registers.Entity = value;
				if ((value != null))
				{
					value.RegistersUsersMap.Add(this);
					_fkRegister = value.RegisterID;
				}
				else
				{
					_fkRegister = default(int);
				}
			}
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
	
	#region Attachment handlers
	private void RegistersView_Attach(RegistersView entity)
	{
		this.SendPropertyChanging();
		entity.RegistersUsersMap = this;
	}
	
	private void RegistersView_Detach(RegistersView entity)
	{
		this.SendPropertyChanging();
		entity.RegistersUsersMap = null;
	}
	#endregion
}

[Table(Name="public.RegistersView")]
public partial class RegistersView : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private int _fkField;
	
	private int _fkRegistersUsersMap;
	
	private int _registerViewID;
	
	private double _weight;
	
	private EntityRef<RegistersUsersMap> _registersUsersMap = new EntityRef<RegistersUsersMap>();
	
	private EntityRef<Fields> _fields = new EntityRef<Fields>();
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnFkFieldChanged();
		
		partial void OnFkFieldChanging(int value);
		
		partial void OnFkRegistersUsersMapChanged();
		
		partial void OnFkRegistersUsersMapChanging(int value);
		
		partial void OnRegisterViewIDChanged();
		
		partial void OnRegisterViewIDChanging(int value);
		
		partial void OnWeightChanged();
		
		partial void OnWeightChanging(double value);
		#endregion
	
	
	public RegistersView()
	{
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_fkField", Name="fk_field", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkField
	{
		get
		{
			return this._fkField;
		}
		set
		{
			if ((_fkField != value))
			{
				this.OnFkFieldChanging(value);
				this.SendPropertyChanging();
				this._fkField = value;
				this.SendPropertyChanged("FkField");
				this.OnFkFieldChanged();
			}
		}
	}
	
	[Column(Storage="_fkRegistersUsersMap", Name="fk_registersusersmap", DbType="integer(32,0)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public int FkRegistersUsersMap
	{
		get
		{
			return this._fkRegistersUsersMap;
		}
		set
		{
			if ((_fkRegistersUsersMap != value))
			{
				this.OnFkRegistersUsersMapChanging(value);
				this.SendPropertyChanging();
				this._fkRegistersUsersMap = value;
				this.SendPropertyChanged("FkRegistersUsersMap");
				this.OnFkRegistersUsersMapChanged();
			}
		}
	}
	
	[Column(Storage="_registerViewID", Name="registerviewid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'registersview_registerviewid_seq\')")]
	[DebuggerNonUserCode()]
	public int RegisterViewID
	{
		get
		{
			return this._registerViewID;
		}
		set
		{
			if ((_registerViewID != value))
			{
				this.OnRegisterViewIDChanging(value);
				this.SendPropertyChanging();
				this._registerViewID = value;
				this.SendPropertyChanged("RegisterViewID");
				this.OnRegisterViewIDChanged();
			}
		}
	}
	
	[Column(Storage="_weight", Name="weight", DbType="double precision", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public double Weight
	{
		get
		{
			return this._weight;
		}
		set
		{
			if ((_weight != value))
			{
				this.OnWeightChanging(value);
				this.SendPropertyChanging();
				this._weight = value;
				this.SendPropertyChanged("Weight");
				this.OnWeightChanged();
			}
		}
	}
	
	#region Parents
	[Association(Storage="_registersUsersMap", OtherKey="RegistersUsersMapID", ThisKey="FkRegistersUsersMap", Name="fk_registersview_registersusersmap", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public RegistersUsersMap RegistersUsersMap
	{
		get
		{
			return this._registersUsersMap.Entity;
		}
		set
		{
			if (((this._registersUsersMap.Entity == value) 
						== false))
			{
				if ((this._registersUsersMap.Entity != null))
				{
					RegistersUsersMap previousRegistersUsersMap = this._registersUsersMap.Entity;
					this._registersUsersMap.Entity = null;
					previousRegistersUsersMap.RegistersView.Remove(this);
				}
				this._registersUsersMap.Entity = value;
				if ((value != null))
				{
					value.RegistersView.Add(this);
					_fkRegistersUsersMap = value.RegistersUsersMapID;
				}
				else
				{
					_fkRegistersUsersMap = default(int);
				}
			}
		}
	}
	
	[Association(Storage="_fields", OtherKey="FieldID", ThisKey="FkField", Name="fk_registersview_fields", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public Fields Fields
	{
		get
		{
			return this._fields.Entity;
		}
		set
		{
			if (((this._fields.Entity == value) 
						== false))
			{
				if ((this._fields.Entity != null))
				{
					Fields previousFields = this._fields.Entity;
					this._fields.Entity = null;
					previousFields.RegistersView.Remove(this);
				}
				this._fields.Entity = value;
				if ((value != null))
				{
					value.RegistersView.Add(this);
					_fkField = value.FieldID;
				}
				else
				{
					_fkField = default(int);
				}
			}
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
}

[Table(Name="public.Struct")]
public partial class Struct : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private System.Nullable<int> _fkParent;
	
	private string _name;
	
	private int _strUCtID;
	
	private EntitySet<Struct> _strUCt;
	
	private EntityRef<Struct> _fkParentStrUCt = new EntityRef<Struct>();
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnFkParentChanged();
		
		partial void OnFkParentChanging(System.Nullable<int> value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnSTRuCtIDChanged();
		
		partial void OnSTRuCtIDChanging(int value);
		#endregion
	
	
	public Struct()
	{
		_strUCt = new EntitySet<Struct>(new Action<Struct>(this.STRuCt_Attach), new Action<Struct>(this.STRuCt_Detach));
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="bit(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_fkParent", Name="fk_parent", DbType="integer(32,0)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public System.Nullable<int> FkParent
	{
		get
		{
			return this._fkParent;
		}
		set
		{
			if ((_fkParent != value))
			{
				this.OnFkParentChanging(value);
				this.SendPropertyChanging();
				this._fkParent = value;
				this.SendPropertyChanged("FkParent");
				this.OnFkParentChanged();
			}
		}
	}
	
	[Column(Storage="_name", Name="name", DbType="character varying(200)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public string Name
	{
		get
		{
			return this._name;
		}
		set
		{
			if (((_name == value) 
						== false))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	[Column(Storage="_strUCtID", Name="structid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'struct_structid_seq\')")]
	[DebuggerNonUserCode()]
	public int STRuCtID
	{
		get
		{
			return this._strUCtID;
		}
		set
		{
			if ((_strUCtID != value))
			{
				this.OnSTRuCtIDChanging(value);
				this.SendPropertyChanging();
				this._strUCtID = value;
				this.SendPropertyChanged("STRuCtID");
				this.OnSTRuCtIDChanged();
			}
		}
	}
	
	#region Children
	[Association(Storage="_strUCt", OtherKey="FkParent", ThisKey="STRuCtID", Name="fk_struct_struct")]
	[DebuggerNonUserCode()]
	public EntitySet<Struct> STRuCt
	{
		get
		{
			return this._strUCt;
		}
		set
		{
			this._strUCt = value;
		}
	}
	#endregion
	
	#region Parents
	[Association(Storage="_fkParentStrUCt", OtherKey="STRuCtID", ThisKey="FkParent", Name="fk_struct_struct", IsForeignKey=true)]
	[DebuggerNonUserCode()]
	public Struct FkParentSTRuCt
	{
		get
		{
			return this._fkParentStrUCt.Entity;
		}
		set
		{
			if (((this._fkParentStrUCt.Entity == value) 
						== false))
			{
				if ((this._fkParentStrUCt.Entity != null))
				{
					Struct previousStruct = this._fkParentStrUCt.Entity;
					this._fkParentStrUCt.Entity = null;
					previousStruct.STRuCt.Remove(this);
				}
				this._fkParentStrUCt.Entity = value;
				if ((value != null))
				{
					value.STRuCt.Add(this);
					_fkParent = value.STRuCtID;
				}
				else
				{
					_fkParent = null;
				}
			}
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
	
	#region Attachment handlers
	private void STRuCt_Attach(Struct entity)
	{
		this.SendPropertyChanging();
		entity.FkParentSTRuCt = this;
	}
	
	private void STRuCt_Detach(Struct entity)
	{
		this.SendPropertyChanging();
		entity.FkParentSTRuCt = null;
	}
	#endregion
}

[Table(Name="public.Users")]
public partial class Users : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
{
	
	private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
	
	private bool _active;
	
	private string _email;
	
	private string _login;
	
	private string _name;
	
	private string _password;
	
	private string _strUCt;
	
	private int _userID;
	
	private EntitySet<Logs> _logs;
	
	private EntitySet<RegistersUsersMap> _registersUsersMap;
	
	#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActiveChanged();
		
		partial void OnActiveChanging(bool value);
		
		partial void OnEmailChanged();
		
		partial void OnEmailChanging(string value);
		
		partial void OnLoginChanged();
		
		partial void OnLoginChanging(string value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		
		partial void OnPasswordChanged();
		
		partial void OnPasswordChanging(string value);
		
		partial void OnSTRuCtChanged();
		
		partial void OnSTRuCtChanging(string value);
		
		partial void OnUserIDChanged();
		
		partial void OnUserIDChanging(int value);
		#endregion
	
	
	public Users()
	{
		_logs = new EntitySet<Logs>(new Action<Logs>(this.Logs_Attach), new Action<Logs>(this.Logs_Detach));
		_registersUsersMap = new EntitySet<RegistersUsersMap>(new Action<RegistersUsersMap>(this.RegistersUsersMap_Attach), new Action<RegistersUsersMap>(this.RegistersUsersMap_Detach));
		this.OnCreated();
	}
	
	[Column(Storage="_active", Name="active", DbType="boolean", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public bool Active
	{
		get
		{
			return this._active;
		}
		set
		{
			if ((_active != value))
			{
				this.OnActiveChanging(value);
				this.SendPropertyChanging();
				this._active = value;
				this.SendPropertyChanged("Active");
				this.OnActiveChanged();
			}
		}
	}
	
	[Column(Storage="_email", Name="email", DbType="character varying(200)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Email
	{
		get
		{
			return this._email;
		}
		set
		{
			if (((_email == value) 
						== false))
			{
				this.OnEmailChanging(value);
				this.SendPropertyChanging();
				this._email = value;
				this.SendPropertyChanged("Email");
				this.OnEmailChanged();
			}
		}
	}
	
	[Column(Storage="_login", Name="login", DbType="character varying(200)", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Login
	{
		get
		{
			return this._login;
		}
		set
		{
			if (((_login == value) 
						== false))
			{
				this.OnLoginChanging(value);
				this.SendPropertyChanging();
				this._login = value;
				this.SendPropertyChanged("Login");
				this.OnLoginChanged();
			}
		}
	}
	
	[Column(Storage="_name", Name="name", DbType="text", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string Name
	{
		get
		{
			return this._name;
		}
		set
		{
			if (((_name == value) 
						== false))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	[Column(Storage="_password", Name="password", DbType="character varying(200)", AutoSync=AutoSync.Never, CanBeNull=false)]
	[DebuggerNonUserCode()]
	public string Password
	{
		get
		{
			return this._password;
		}
		set
		{
			if (((_password == value) 
						== false))
			{
				this.OnPasswordChanging(value);
				this.SendPropertyChanging();
				this._password = value;
				this.SendPropertyChanged("Password");
				this.OnPasswordChanged();
			}
		}
	}
	
	[Column(Storage="_strUCt", Name="struct", DbType="text", AutoSync=AutoSync.Never)]
	[DebuggerNonUserCode()]
	public string STRuCt
	{
		get
		{
			return this._strUCt;
		}
		set
		{
			if (((_strUCt == value) 
						== false))
			{
				this.OnSTRuCtChanging(value);
				this.SendPropertyChanging();
				this._strUCt = value;
				this.SendPropertyChanged("STRuCt");
				this.OnSTRuCtChanged();
			}
		}
	}
	
	[Column(Storage="_userID", Name="userid", DbType="integer(32,0)", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false, Expression="nextval(\'users_userid_seq\')")]
	[DebuggerNonUserCode()]
	public int UserID
	{
		get
		{
			return this._userID;
		}
		set
		{
			if ((_userID != value))
			{
				this.OnUserIDChanging(value);
				this.SendPropertyChanging();
				this._userID = value;
				this.SendPropertyChanged("UserID");
				this.OnUserIDChanged();
			}
		}
	}
	
	#region Children
	[Association(Storage="_logs", OtherKey="FkUsers", ThisKey="UserID", Name="fk_logs_users")]
	[DebuggerNonUserCode()]
	public EntitySet<Logs> Logs
	{
		get
		{
			return this._logs;
		}
		set
		{
			this._logs = value;
		}
	}
	
	[Association(Storage="_registersUsersMap", OtherKey="FkUser", ThisKey="UserID", Name="fk_registersusersmap_users")]
	[DebuggerNonUserCode()]
	public EntitySet<RegistersUsersMap> RegistersUsersMap
	{
		get
		{
			return this._registersUsersMap;
		}
		set
		{
			this._registersUsersMap = value;
		}
	}
	#endregion
	
	public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
	
	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
		if ((h != null))
		{
			h(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(string propertyName)
	{
		System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
		if ((h != null))
		{
			h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
	
	#region Attachment handlers
	private void Logs_Attach(Logs entity)
	{
		this.SendPropertyChanging();
		entity.Users = this;
	}
	
	private void Logs_Detach(Logs entity)
	{
		this.SendPropertyChanging();
		entity.Users = null;
	}
	
	private void RegistersUsersMap_Attach(RegistersUsersMap entity)
	{
		this.SendPropertyChanging();
		entity.Users = this;
	}
	
	private void RegistersUsersMap_Detach(RegistersUsersMap entity)
	{
		this.SendPropertyChanging();
		entity.Users = null;
	}
	#endregion
}
