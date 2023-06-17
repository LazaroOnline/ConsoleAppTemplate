﻿using System.ComponentModel.DataAnnotations;

namespace ConsoleAppTemplate;

public class AppSettings
{
	public SomeExampleConfigSection SomeConfigSection { get; set; }
	// ...
}

// TODO: remove this config class example:
public class SomeExampleConfigSection
{

	[Url]
	[Required]
	public string SomeUrl { get; set; }

	[Required(ErrorMessage = "{0} is required.")]
	[StringLength(50, MinimumLength = 3, ErrorMessage = "Name should be minimum 3 characters and a maximum of 50 characters.")]
	public string SomeName { get; set; }

	[Range(0, Int32.MaxValue)]
	public int SomeInt { get; set; }

	public List<string> SomeStringList { get; set; }
}
